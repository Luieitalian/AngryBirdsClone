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

        private enum SlingshotState
        {
            None = 0, Idle, Holding, Reloading
        }

        private SlingshotState state;

        void Start()
        {
            state = SlingshotState.Idle;

            leftLineRenderer = transform.Find("LeftLine").GetComponent<LineRenderer>();
            rightLineRenderer = transform.Find("RightLine").GetComponent<LineRenderer>();
            leftLineRenderer.enabled = false;
            rightLineRenderer.enabled = false;

            centerPos = transform.Find("CenterPivot").position;
            StartCoroutine(InstantiateBird(centerPos, 0f));
        }

        void Update()
        {
            switch (state)
            {
                case SlingshotState.Idle:
                    if (Mouse.current.leftButton.wasPressedThisFrame && IsWithinSlingshotArea())
                        state = SlingshotState.Holding;
                    break;
                case SlingshotState.Holding:
                    if (Mouse.current.leftButton.isPressed)
                    {
                        touchPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                        touchPos = centerPos + Vector2.ClampMagnitude(touchPos - centerPos, slingRange);

                        DrawSlings();
                        SetBirdPosRotation();
                    }
                    else if (Mouse.current.leftButton.wasReleasedThisFrame)
                    {
                        LaunchBird(spawnedBird);
                        SetLines(centerPos);

                        state = SlingshotState.Reloading;

                        StartCoroutine(InstantiateBird(centerPos, 2f));
                    }
                    break;
                case SlingshotState.Reloading:
                    break;
            }
        }

        void DrawSlings()
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;

            SetLines(touchPos);
        }

        void SetLines(Vector2 pos)
        {
            leftLineRenderer.SetPosition(1, pos);
            rightLineRenderer.SetPosition(1, pos);

            leftLineRenderer.SetPosition(0, leftLineRenderer.transform.position);
            rightLineRenderer.SetPosition(0, rightLineRenderer.transform.position);
        }

        bool IsWithinSlingshotArea()
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Physics2D.OverlapPoint(touchPos, slingshotAreaMask)) return true;
            else return false;
        }

        IEnumerator InstantiateBird(Vector2 pos, float time)
        {
            yield return new WaitForSeconds(time);
            spawnedBird = Instantiate(redBirdPrefab, pos, Quaternion.identity);

            state = SlingshotState.Idle;
        }

        void LaunchBird(BaseBird bird)
        {
            bird.Launch(centerPos - touchPos);
        }

        void SetBirdPosRotation()
        {
            spawnedBird.transform.position = touchPos + new Vector2(0.15f, 0);
            spawnedBird.transform.right = centerPos - touchPos;
        }
    }
}
