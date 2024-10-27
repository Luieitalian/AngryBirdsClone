using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class BombBird : BaseBird
    {
        [SerializeField] private float abilityDamage;
        [SerializeField] private float abilityImpulsePower;
        [SerializeField] private LayerMask abilityEffectAreaMask;
        [SerializeField] private ParticleSystem abilityEffect;

        private CircleCollider2D abilityEffectArea;
        private ContactFilter2D contactFilter2D; // To use with Collider2D.OverlapCollider in UseAbility

        void Awake()
        {
            controls = new Controls();

            contactFilter2D = new ContactFilter2D();
            contactFilter2D.useLayerMask = true;
            contactFilter2D.SetLayerMask(abilityEffectAreaMask);

            renderer2D = GetComponent<SpriteRenderer>();
            abilityEffectArea = transform.Find("AbilityEffectArea").GetComponent<CircleCollider2D>();

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
                    collider.GetComponent<TargetObject>().Health.TakeDamage(abilityDamage);
                    Vector2 impulseVector = (collider.transform.position - transform.position).normalized;
                    collider.GetComponent<Rigidbody2D>().AddForce(impulseVector * abilityImpulsePower, ForceMode2D.Impulse);
                }

                Instantiate(abilityEffect, transform.position, transform.rotation, transform.parent);
                print("Bombbird using ability!");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            hasCollided = true;
            renderer2D.sprite = collidedSprite;
        }
    }
}
