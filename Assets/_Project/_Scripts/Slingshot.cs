using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace berkepite
{
    public class Slingshot : MonoBehaviour
    {
        private LineRenderer leftLineRenderer;
        private LineRenderer rightLineRenderer;
        private Vector2 centerPos;

        [SerializeField]
        private float slingRange;
        [SerializeField]
        private LayerMask slingshotAreaMask;

        [SerializeField]
        private RedBird redBirdPrefab;
        private BaseBird spawnedBird;
        private Vector2 touchPos;

        private Controls controls;
        private InputAction touchAction;
        private InputAction touchPosition;

        private enum SlingshotState
        {
            None = 0, Initialising, Idle, Holding, Reloading
        }
        private SlingshotState state = SlingshotState.None;

        void Awake()
        {
            controls = new Controls();
        }

        void OnEnable()
        {
            touchPosition = controls.Player.Position;
            touchAction = controls.Player.Touch;
            touchPosition.Enable();
            touchAction.Enable();
        }

        void OnDisable()
        {
            touchPosition.Disable();
            touchAction.Disable();
        }

        void Start()
        {
            state = SlingshotState.Initialising;

            leftLineRenderer = transform.Find("LeftLine").GetComponent<LineRenderer>();
            rightLineRenderer = transform.Find("RightLine").GetComponent<LineRenderer>();
            leftLineRenderer.enabled = false;
            rightLineRenderer.enabled = false;

            centerPos = transform.Find("CenterPivot").position;
        }

        public void OnTargetsInitFinished()
        {
            StartCoroutine(InstantiateBird(centerPos, 0f));
            state = SlingshotState.Idle;
        }

        void Update()
        {
            switch (state)
            {
                case SlingshotState.Initialising:
                    break;
                case SlingshotState.Idle:
                    if (touchAction.IsPressed() && IsWithinSlingshotArea())
                        state = SlingshotState.Holding;
                    break;
                case SlingshotState.Holding:
                    if (touchAction.IsPressed())
                    {
                        touchPos = Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>());
                        touchPos = centerPos + Vector2.ClampMagnitude(touchPos - centerPos, slingRange);

                        DrawSlings();
                        SetBirdPosRotation();
                    }
                    else
                    {
                        LaunchBird(spawnedBird);
                        SetLines(centerPos);

                        state = SlingshotState.Reloading;

                        StartCoroutine(InstantiateBird(centerPos, 2f));
                        state = SlingshotState.Idle;
                    }
                    break;
                case SlingshotState.Reloading:
                    break;
            }
        }

        private void DrawSlings()
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;

            SetLines(touchPos);
        }

        private void SetLines(Vector2 pos)
        {
            leftLineRenderer.SetPosition(1, pos);
            rightLineRenderer.SetPosition(1, pos);

            leftLineRenderer.SetPosition(0, leftLineRenderer.transform.position);
            rightLineRenderer.SetPosition(0, rightLineRenderer.transform.position);
        }

        private bool IsWithinSlingshotArea()
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touchPosition.ReadValue<Vector2>());

            if (Physics2D.OverlapPoint(touchPos, slingshotAreaMask)) return true;
            else return false;
        }

        private IEnumerator InstantiateBird(Vector2 pos, float time)
        {
            yield return new WaitForSeconds(time);
            spawnedBird = Instantiate(redBirdPrefab, pos, Quaternion.identity);
        }

        private void LaunchBird(BaseBird bird)
        {
            bird.Launch(centerPos - touchPos);
        }

        private void SetBirdPosRotation()
        {
            spawnedBird.transform.position = touchPos + new Vector2(0.15f, 0);
            spawnedBird.transform.right = centerPos - touchPos;
        }
    }
}
