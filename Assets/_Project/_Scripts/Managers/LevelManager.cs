using System;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<BaseBird> remainingBirds;
        [SerializeField] private GameObject remainingBirdSlotPrefab;

        private int remainingBirdsCounter;
        private GameObject remainingBirdsUI;
        private LevelManagerState currentState = new LevelManagerNone();

        private List<GameObject> targetObjects;

        private int targetsFinishedCount = 0;
        private int pigCount = 0;

        private SceneLoader sceneLoader;
        private Action OnTargetsFinishedEvent;
        private Action OnLevelLost;
        private Action OnLevelWon;

        public SceneLoader SceneLoader
        {
            get { return sceneLoader; }
            private set { sceneLoader = value; }
        }

        void Awake()
        {
            remainingBirdsUI = GameObject.Find("RemainingBirdsUI");

            SetRemainingBirdsUI();

            sceneLoader = FindObjectOfType<SceneLoader>();

            var slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
            OnLevelLost += slingshot.OnLevelLost;
            OnTargetsFinishedEvent += slingshot.OnTargetsInitFinished;
        }

        void Start()
        {
            // Get Targets & add target pig counts
            targetObjects = new List<GameObject>();
            var targets = FindObjectsOfType<Target>();
            foreach (var target in targets)
            {
                targetObjects.Add(target.gameObject);
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
            foreach (var target in targetObjects)
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
            if (++targetsFinishedCount == targetObjects.Count)
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
            if (remainingBirdsCounter < remainingBirds.Count)
            {
                var newBird = Instantiate(remainingBirds[remainingBirdsCounter++], pos, Quaternion.identity, transform.root);
                UpdateRemainingBirdsUI();
                return newBird;
            }
            else
            {
                OnLevelLost.Invoke();
                Debug.Log("Level Lost!");
                return null;
            }
        }

        public void UpdateRemainingBirdsUI()
        {
            Destroy(remainingBirdsUI.transform.GetChild(0).gameObject);
        }

        private void SetRemainingBirdsUI()
        {
            if (remainingBirds.Count == 0)
            {
                Debug.LogError("There is no remaining birds in 'remainingBirds' variable!\nPlease add birds!");
                Debug.Break();
            }

            foreach (var item in remainingBirds)
            {
                var slot = Instantiate(remainingBirdSlotPrefab, Vector3.zero, Quaternion.identity, remainingBirdsUI.transform);
                slot.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            }
        }

    }
}
