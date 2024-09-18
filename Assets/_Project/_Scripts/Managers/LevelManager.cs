using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace berkepite
{
    public class LevelManager : MonoBehaviour
    {
        private enum LevelState
        {
            None = 0, Woke, Initialising, Playing, Won, Lost
        }
        private static LevelState levelState = LevelState.None;
        private List<GameObject> targetObjects;

        private int targetsFinishedCount = 0;

        [SerializeField] private UnityEvent OnTargetsFinishedEvent;

        void Awake()
        {
            targetObjects = new List<GameObject>();

            var targets = FindObjectsOfType<Target>();
            foreach (var item in targets)
            {
                targetObjects.Add(item.gameObject);
                print(item.gameObject.name);
            }

            levelState = LevelState.Woke;
        }

        void Update()
        {
            switch (levelState)
            {
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

    }
}
