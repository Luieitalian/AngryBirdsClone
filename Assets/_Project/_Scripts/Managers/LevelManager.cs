using System;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<BaseBird> initialBirds;
        [SerializeField] private RemainingBirdsUI _remainingBirdsUI;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private Slingshot _slingshot;

        private int targetsFinishedCount = 0;
        private int usedBirdsCounter = 0;
        private int pigCount = 0;

        private LevelManagerState currentState = new LevelManagerNone();
        private List<GameObject> _targetObjects;

        public Action OnLevelLost;
        private Action OnTargetsFinishedEvent;

        public SceneLoader SceneLoader
        {
            get { return _sceneLoader; }
            private set { _sceneLoader = value; }
        }

        public List<BaseBird> InitialBirds
        {
            get { return initialBirds; }
            private set { initialBirds = value; }
        }

        public int PigCount
        {
            get { return pigCount; }
            private set { pigCount = value; }
        }

        public int UsedBirdsCounter
        {
            get { return usedBirdsCounter; }
            private set { usedBirdsCounter = value; }
        }

        void Awake()
        {
            _remainingBirdsUI.SetUI();

            OnLevelLost += _slingshot.OnLevelLost;
            OnTargetsFinishedEvent += _slingshot.OnTargetsInitFinished;
        }

        void Start()
        {
            // Get Targets & add target pig counts
            _targetObjects = new List<GameObject>();
            var targets = FindObjectsOfType<Target>();
            foreach (var target in targets)
            {
                _targetObjects.Add(target.gameObject);
                pigCount += target.PigCount;
            }
            ChangeState(new LevelManagerWoke());
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        public void WakeUpTargets()
        {
            foreach (var target in _targetObjects)
            {
                target.GetComponent<Target>().WakeUp();
            }
        }

        public void ChangeState(LevelManagerState state)
        {
            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }

        public void OnTargetFinished()
        {
            if (++targetsFinishedCount == _targetObjects.Count)
            {
                ChangeState(new LevelManagerPlaying());
                OnTargetsFinishedEvent.Invoke();
            }
        }

        public void OnPigDeath()
        {
            if (--pigCount == 0) ChangeState(new LevelManagerWon());
        }

        public BaseBird RequestBird(Vector2 pos)
        {
            if (usedBirdsCounter < initialBirds.Count)
            {
                var newBird = Instantiate(initialBirds[usedBirdsCounter++], pos, Quaternion.identity, transform.root);
                _remainingBirdsUI.UpdateUI();
                return newBird;
            }
            else
            {
                if (currentState is not LevelManagerWon)
                    ChangeState(new LevelManagerWaiting());
                return null;
            }
        }
    }
}
