using UnityEngine;

namespace berkepite
{
    public class RedBird : BaseBird
    {
        [SerializeField] private float launchPower;

        private Rigidbody2D rigidBody2D;
        private CircleCollider2D circleCollider2D;

        public override float LaunchPower { get { return launchPower; } protected set { launchPower = value; } }

        void Awake()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

            circleCollider2D = GetComponent<CircleCollider2D>();
            circleCollider2D.enabled = false;
        }

        void FixedUpdate()
        {
            if (isLaunched && !hasCollided) transform.right = rigidBody2D.velocity;
        }

        public override void Launch(Vector2 force)
        {
            rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidBody2D.AddForce(force * launchPower, ForceMode2D.Impulse);

            isLaunched = true;
            circleCollider2D.enabled = true;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            hasCollided = true;
        }

    }
}
