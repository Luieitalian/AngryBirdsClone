using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class RedBird : BaseBird
    {
        [SerializeField] private float birdAbilityPower = 1;
        [SerializeField] private ParticleSystem abilityEffect;
        [SerializeField] private LayerMask abilityEffectAreaMask;

        private PolygonCollider2D abilityEffectArea;
        private ContactFilter2D contactFilter2D; // To use with Collider2D.OverlapCollider in UseAbility

        void Awake()
        {
            controls = new Controls();

            contactFilter2D = new ContactFilter2D();
            contactFilter2D.useLayerMask = true;
            contactFilter2D.SetLayerMask(abilityEffectAreaMask);

            renderer2D = GetComponent<SpriteRenderer>();

            abilityEffectArea = transform.Find("AbilityEffectArea").GetComponent<PolygonCollider2D>();

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
                hasUsedPower = true;
                List<Collider2D> colliders = new List<Collider2D>(6);

                abilityEffectArea.OverlapCollider(contactFilter2D, colliders);

                foreach (var collider in colliders)
                {
                    collider.GetComponent<Rigidbody2D>().AddForce(Vector2.right * birdAbilityPower, ForceMode2D.Impulse);
                }

                Instantiate(abilityEffect, transform.position, transform.rotation, transform.parent);
                print("Redbird using ability!");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            hasCollided = true;
            renderer2D.sprite = collidedSprite;
        }
    }
}
