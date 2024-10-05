using UnityEngine;

namespace berkepite
{
    public class ChuckBird : BaseBird
    {
        [SerializeField] private float abilityPower;
        [SerializeField] private Sprite speedingSprite;

        void Awake()
        {
            controls = new Controls();

            renderer2D = GetComponent<SpriteRenderer>();

            rigidBody2D = GetComponent<Rigidbody2D>();
            rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

            circleCollider2D = GetComponent<CircleCollider2D>();
            circleCollider2D.enabled = false;
        }

        private void OnEnable()
        {
            birdAbilityTouchAction = controls.Player.Touch;
            birdAbilityTouchAction.Enable();
        }

        private void OnDisable()
        {
            birdAbilityTouchAction.Disable();
        }

        void FixedUpdate()
        {
            if (isLaunched && !hasCollided) transform.right = rigidBody2D.velocity;
        }

        void Update()
        {
            if (birdAbilityTouchAction.WasPressedThisFrame())
                UseAbility();
        }

        public override void UseAbility()
        {
            if (!hasUsedPower && isLaunched && !hasCollided)
            {
                rigidBody2D.gravityScale = 0f;

                Vector2 abilityVector = rigidBody2D.velocity.normalized * abilityPower;
                rigidBody2D.AddForce(abilityVector, ForceMode2D.Impulse);

                renderer2D.sprite = speedingSprite;

                print("Chuckbird using ability!");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            hasCollided = true;
            rigidBody2D.gravityScale = 1f;
            renderer2D.sprite = collidedSprite;
        }
    }
}
