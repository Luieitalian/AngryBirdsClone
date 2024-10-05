using UnityEngine;
using UnityEngine.InputSystem;

namespace berkepite
{
    public abstract class BaseBird : MonoBehaviour
    {
        [SerializeField] protected Sprite collidedSprite;
        public float launchPower = 8;

        protected bool isLaunched = false;
        protected bool hasCollided = false;
        protected bool hasUsedPower = false;

        protected SpriteRenderer renderer2D;
        protected Rigidbody2D rigidBody2D;
        protected CircleCollider2D circleCollider2D;

        protected Controls controls;
        protected InputAction birdAbilityTouchAction;

        public void Launch(Vector2 force)
        {
            rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidBody2D.AddForce(force * launchPower, ForceMode2D.Impulse);

            isLaunched = true;
            circleCollider2D.enabled = true;
        }
        public abstract void UseAbility();
    }
}
