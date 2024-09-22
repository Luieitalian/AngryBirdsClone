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
        private Collider2D _collider;
        private Animation _animation;
        private SpriteRenderer _spriteRenderer;

        public CHealth Health
        {
            get { return m_Health; }
            private set { m_Health = value; }
        }

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.simulated = false;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;

            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;

            m_Health.Init();

            m_Health.OnCriticalHealth(OnCriticalHealth);
            m_Health.OnHealthDepleted(OnHealthDepleted);

            _animation = GetComponent<Animation>();
        }

        public void AddAnim(AnimationClip clip, string name)
        {
            _animation.AddClip(clip, name);
        }

        public void PlayAnim(string name)
        {
            _animation.Play(name);
        }

        public void Init()
        {
            if (spawnEffect)
                Instantiate(spawnEffect, transform.position, Quaternion.identity, transform);

            if (_animation)
                _animation.Play("initAnim");

            StartCoroutine(EnableSprite(visibleDelay));
        }

        public void EnablePhysics()
        {
            _rigidbody2D.simulated = true;
            _collider.enabled = true;
        }

        private IEnumerator EnableSprite(float time)
        {
            yield return new WaitForSeconds(time);
            _spriteRenderer.enabled = true;
        }

        private void OnHealthDepleted()
        {
            if (dieEffect)
                Instantiate(dieEffect, transform.position, Quaternion.identity, transform.root);
        }

        private void OnCriticalHealth()
        {
            if (criticalHealthSprite)
                _spriteRenderer.sprite = criticalHealthSprite;
        }
    }
}
