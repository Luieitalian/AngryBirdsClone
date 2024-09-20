using System;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class LevelManager : MonoBehaviour
    {
        private LevelManagerState currentState = new LevelManagerNone();

        private List<GameObject> targetObjects;

        private int targetsFinishedCount = 0;
        private int pigCount = 0;

        private SceneLoader sceneLoader;
        private Action OnTargetsFinishedEvent;

        public SceneLoader SceneLoader
        {
            get { return sceneLoader; }
            private set { sceneLoader = value; }
        }

        void Awake()
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

            var slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
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

    }
}
