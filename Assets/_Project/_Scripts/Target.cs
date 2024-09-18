using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace berkepite
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnFinishedEvent;

        private enum TargetState
        {
            None = 0, Woke, Initialising, Finished
        }
        private static TargetState targetState = TargetState.None;
        private List<GameObject> objects;

        void Awake()
        {
            objects = new List<GameObject>();
            foreach (Transform child in transform)
            {
                objects.Add(child.gameObject);
            }
        }

        void Update()
        {
            switch (targetState)
            {
                case TargetState.None: break;
                case TargetState.Woke:
                    StartCoroutine(InitializeTargetObjects(.25f));
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
            StartCoroutine(EnablePhysicsOnTargetObjects(.05f));
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
