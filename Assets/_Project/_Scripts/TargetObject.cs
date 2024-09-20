using System.Collections;
using UnityEngine;

namespace berkepite
{
    public class TargetObject : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        [SerializeField] private float visibleDelay = .1f;
        [SerializeField] private ParticleSystem spawnEffect;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.simulated = false;

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;

            TryGetComponent(out animator);
        }

        public void Init()
        {
            if (spawnEffect)
                Instantiate(spawnEffect, transform.position, Quaternion.identity, transform);

            if (animator)
                animator.SetTrigger("Init");

            StartCoroutine(EnableSprite(visibleDelay));
        }

        public void EnablePhysics()
        {
            _rigidbody2D.simulated = true;
        }

        private IEnumerator EnableSprite(float time)
        {
            yield return new WaitForSeconds(time);
            spriteRenderer.enabled = true;
        }

    }
}
