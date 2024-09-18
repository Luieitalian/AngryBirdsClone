using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float initializeInterval;
        [SerializeField] private float physicsInitializeInterval;

        private enum TargetState
        {
            None = 0, Woke, Initialising, Finished
        }
        private TargetState targetState = TargetState.None;
        private List<GameObject> objects;
        private int pigCount;

        private Action OnFinishedEvent;

        public int PigCount
        {
            get { return pigCount; }
            private set { pigCount = value; }
        }

        void Awake()
        {
            var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            OnFinishedEvent += levelManager.OnTargetFinished;

            pigCount = 0;

            objects = new List<GameObject>();
            foreach (Transform child in transform)
            {
                objects.Add(child.gameObject);
                if (child.gameObject.CompareTag("Pig")) pigCount++;
            }
        }

        void Update()
        {
            switch (targetState)
            {
                case TargetState.None: break;
                case TargetState.Woke:
                    StartCoroutine(InitializeTargetObjects(initializeInterval));
                    targetState = TargetState.Initialising;
                    break;
                case TargetState.Initialising:
                    break;
                case TargetState.Finished:
                    OnFinishedEvent.Invoke();
                    targetState = TargetState.None;
                    break;
            }
        }

        public void WakeUp()
        {
            targetState = TargetState.Woke;
        }

        private IEnumerator InitializeTargetObjects(float time)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                yield return new WaitForSeconds(time);
                objects[i].GetComponent<TargetObject>().Init();
            }
            StartCoroutine(EnablePhysicsOnTargetObjects(physicsInitializeInterval));
        }

        private IEnumerator EnablePhysicsOnTargetObjects(float time)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                yield return new WaitForSeconds(time);
                objects[i].GetComponent<TargetObject>().EnablePhysics();
            }
            targetState = TargetState.Finished;
        }

    }
}
