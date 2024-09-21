using System.Collections;
using UnityEngine;

namespace berkepite
{
    public class TargetObject : MonoBehaviour
    {
        [SerializeField] private CHealth m_Health;
        [SerializeField] private float visibleDelay = .1f;
        [SerializeField] private ParticleSystem spawnEffect;
        [SerializeField] private ParticleSystem dieEffect;
        [SerializeField] private Sprite criticalHealthSprite;

        private Rigidbody2D _rigidbody2D;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        public CHealth Health
        {
            get { return m_Health; }
            private set { m_Health = value; }
        }

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.simulated = false;

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;

            m_Health.Init();

            m_Health.OnCriticalHealth(OnCriticalHealth);
            m_Health.OnHealthDepleted(OnHealthDepleted);

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

        private void OnHealthDepleted()
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, Quaternion.identity, transform.root);
        }

        private void OnCriticalHealth()
        {
            if (criticalHealthSprite)
                spriteRenderer.sprite = criticalHealthSprite;
        }
    }
}
