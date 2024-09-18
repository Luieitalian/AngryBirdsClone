using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace berkepite
{
    public class LevelManager : MonoBehaviour
    {
        private enum LevelState
        {
            None = 0, Woke, Initialising, Playing, Won, Lost
        }
        private LevelState levelState = LevelState.None;
        private List<GameObject> targetObjects;

        private int targetsFinishedCount = 0;
        private int pigCount = 0;

        private SceneLoader sceneLoader;
        private Action OnTargetsFinishedEvent;

        void Awake()
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

            var slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
            OnTargetsFinishedEvent += slingshot.OnTargetsInitFinished;

            targetObjects = new List<GameObject>();
            var targets = FindObjectsOfType<Target>();
            foreach (var target in targets)
            {
                targetObjects.Add(target.gameObject);
                pigCount += target.PigCount;
            }

            levelState = LevelState.Woke;
        }

        void Update()
        {
            switch (levelState)
            {
                case LevelState.None:
                    break;
                case LevelState.Woke:
                    foreach (var item in targetObjects)
                    {
                        item.GetComponent<Target>().WakeUp();
                    }
                    levelState = LevelState.Initialising;
                    break;
                case LevelState.Initialising:
                    break;
                case LevelState.Playing:
                    break;
                case LevelState.Won:
                    print("Won!");
                    sceneLoader.LoadScene(0);
                    levelState = LevelState.None;
                    break;
            }
        }

        public void OnTargetFinished()
        {
            if (++targetsFinishedCount == targetObjects.Count)
            {
                levelState = LevelState.Playing;
                OnTargetsFinishedEvent.Invoke();
            }
        }

        public void OnPigDeath()
        {
            if (--pigCount == 0) levelState = LevelState.Won;
        }

    }
}
