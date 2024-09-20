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

        private List<GameObject> childrenList;
        private int pigCount;
        private TargetState currentState = new TargetNone();

        public Action OnFinishedEvent;

        public int PigCount
        {
            get { return pigCount; }
            private set { pigCount = value; }
        }
        public float InitializeInterval
        {
            get { return initializeInterval; }
            private set { initializeInterval = value; }
        }
        public float PhysicsInitializeInterval
        {
            get { return physicsInitializeInterval; }
            private set { physicsInitializeInterval = value; }
        }

        void Awake()
        {
            var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            OnFinishedEvent += levelManager.OnTargetFinished;

            pigCount = 0;

            childrenList = new List<GameObject>();
            foreach (Transform child in transform)
            {
                childrenList.Add(child.gameObject);
                if (child.gameObject.CompareTag("Pig")) pigCount++;
            }
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        public void WakeUp()
        {
            ChangeState(new TargetWoke());
        }

        public void Init()
        {
            StartCoroutine(InitializeTargetObjects(InitializeInterval));
        }

        public void ChangeState(TargetState state)
        {
            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }

        private IEnumerator InitializeTargetObjects(float time)
        {
            for (int i = 0; i < childrenList.Count; i++)
            {
                yield return new WaitForSeconds(time);
                childrenList[i].GetComponent<TargetObject>().Init();
            }
            StartCoroutine(EnablePhysicsOnTargetObjects(physicsInitializeInterval));
        }

        private IEnumerator EnablePhysicsOnTargetObjects(float time)
        {
            for (int i = 0; i < childrenList.Count; i++)
            {
                yield return new WaitForSeconds(time);
                childrenList[i].GetComponent<TargetObject>().EnablePhysics();
            }
            ChangeState(new TargetFinished());
        }

    }
}
