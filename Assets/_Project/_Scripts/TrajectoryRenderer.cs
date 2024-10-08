using UnityEngine;

namespace berkepite
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private int resolution = 12; // Number of points in the path
        [SerializeField] private float timeStep = 0.06f; // Time between each point

        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            Disable();
        }

        public void Disable()
        {
            lineRenderer.enabled = false;
        }

        public void Enable()
        {
            lineRenderer.enabled = true;
        }

        public void DrawPath(Vector2 startPos, Vector2 velocity)
        {
            lineRenderer.positionCount = 0;

            Vector3[] pathPoints = new Vector3[resolution];

            for (int i = 0; i < resolution; i++)
            {
                float t = i * timeStep;

                Vector2 point = CalculatePosition(startPos, velocity, t);
                pathPoints[i] = new Vector3(point.x, point.y, 0);
            }

            lineRenderer.positionCount = resolution;
            lineRenderer.SetPositions(pathPoints);
        }

        private Vector2 CalculatePosition(Vector2 startPos, Vector2 velocity, float time)
        {
            float x = startPos.x + velocity.x * time;
            float y = startPos.y + velocity.y * time - 0.5f * -Physics2D.gravity.y * Mathf.Pow(time, 2);
            return new Vector2(x, y);
        }
    }
}
