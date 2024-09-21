using UnityEngine;

namespace berkepite
{
    public class Pig : MonoBehaviour
    {
        private TargetObject m_TargetObject;

        void Awake()
        {
            m_TargetObject = GetComponent<TargetObject>();

            var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            m_TargetObject.Health.OnHealthDepleted(Die);
            m_TargetObject.Health.OnHealthDepleted(levelManager.OnPigDeath);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Bird"))
                m_TargetObject.Health.TakeDamage(100f);
            else
                m_TargetObject.Health.TakeDamage(collision.relativeVelocity.magnitude);
        }
    }
}
