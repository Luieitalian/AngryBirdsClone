using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace berkepite
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] private float slingRange;
        [SerializeField] private float reloadTime;
        [SerializeField] private LayerMask slingshotAreaMask;
        [SerializeField] private RedBird redBirdPrefab;
        [SerializeField] private AnimationCurve elasticCurve;

        private Vector2 elasticAnimatingPosition; // For the slings to be animated when released.

        private BaseBird spawnedBird;

        private Vector2 slingsPosition;
        private Vector2 centerPosition;
        private Vector2 launchVector;

        private TrajectoryRenderer trajectoryRenderer;
        private LineRenderer leftLineRenderer;
        private LineRenderer rightLineRenderer;

        private Controls controls;
        private InputAction touchAction;
        private InputAction touchPositionAction;

        private SlingshotState currentState = new SlingshotNone();

        public float ReloadTime
        {
            get { return reloadTime; }
            private set { reloadTime = value; }
        }

        public Vector2 TouchPosition
        {
            get { return slingsPosition; }
            set { slingsPosition = value; }
        }

        public InputAction TouchAction
        {
            get { return touchAction; }
            private set { touchAction = value; }
        }

        public InputAction TouchPositionAction
        {
            get { return touchPositionAction; }
            private set { touchPositionAction = value; }
        }

        public TrajectoryRenderer TrajectoryRenderer
        {
            get { return trajectoryRenderer; }
            private set { trajectoryRenderer = value; }
        }

        void Awake()
        {
            ChangeState(new SlingshotInitialising());

            controls = new Controls();
            elasticAnimatingPosition = Vector2.zero;

            trajectoryRenderer = transform.Find("TrajectoryRenderer").GetComponent<TrajectoryRenderer>();

            leftLineRenderer = transform.Find("LeftLine").GetComponent<LineRenderer>();
            rightLineRenderer = transform.Find("RightLine").GetComponent<LineRenderer>();
            leftLineRenderer.enabled = false;
            rightLineRenderer.enabled = false;
        }

        void Start()
        {
            centerPosition = transform.Find("CenterPivot").position;
        }

        void OnEnable()
        {
            touchPositionAction = controls.Player.Position;
            touchAction = controls.Player.Touch;
            touchPositionAction.Enable();
            touchAction.Enable();
        }

        void OnDisable()
        {
            touchPositionAction.Disable();
            touchAction.Disable();
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        public void HandleHolding()
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
            slingsPosition = centerPosition + Vector2.ClampMagnitude(touchPos - centerPosition, slingRange);
            launchVector = centerPosition - slingsPosition;

            TrajectoryRenderer.DrawPath(slingsPosition, launchVector * spawnedBird.LaunchPower);

            DrawSlings();
            SetBirdPosRotation();
        }

        public void HandleReleased()
        {
            LaunchBird(spawnedBird);
            AnimateSlingsTo(centerPosition);

            ChangeState(new SlingshotReloading());
        }

        public void Reload()
        {
            StartCoroutine(ReloadBird(ReloadTime));
        }

        public void ChangeState(SlingshotState state)
        {
            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }

        public void OnTargetsInitFinished()
        {
            InstantiateBird(centerPosition);
            ChangeState(new SlingshotIdle());
        }

        public bool IsWithinSlingshotArea()
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());

            if (Physics2D.OverlapPoint(touchPos, slingshotAreaMask)) return true;
            else return false;
        }

        private IEnumerator ReloadBird(float time)
        {
            yield return new WaitForSeconds(time);
            InstantiateBird(centerPosition);
            ChangeState(new SlingshotIdle());
        }

        private void DrawSlings()
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;

            SetLines(slingsPosition);
        }

        private void SetLines(Vector2 pos)
        {
            leftLineRenderer.SetPosition(1, pos);
            rightLineRenderer.SetPosition(1, pos);

            leftLineRenderer.SetPosition(0, leftLineRenderer.transform.position);
            rightLineRenderer.SetPosition(0, rightLineRenderer.transform.position);
        }

        private void InstantiateBird(Vector2 pos)
        {
            spawnedBird = Instantiate(redBirdPrefab, pos, Quaternion.identity);
        }

        private void LaunchBird(BaseBird bird)
        {
            bird.Launch(launchVector);
        }

        private void SetBirdPosRotation()
        {
            spawnedBird.transform.position = slingsPosition + new Vector2(0.15f, 0);
            spawnedBird.transform.right = launchVector;
        }

        private void AnimateSlingsTo(Vector2 pos)
        {
            elasticAnimatingPosition = leftLineRenderer.GetPosition(1);

            DOTween.To(() => elasticAnimatingPosition, (val) => elasticAnimatingPosition = val, pos, 1f).SetEase(elasticCurve);
            StartCoroutine(AnimateSlings(1f));
        }

        private IEnumerator AnimateSlings(float time)
        {
            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                SetLines(elasticAnimatingPosition);

                yield return null;
            }
        }
    }
}
