using UnityEngine;

namespace berkepite
{
    public class Structure : MonoBehaviour
    {
        private TargetObject m_TargetObject;

        void Awake()
        {
            m_TargetObject = GetComponent<TargetObject>();

            m_TargetObject.Health.OnHealthDepleted(Destroy);
        }

        private void Destroy() { Destroy(gameObject); }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_TargetObject.Health.TakeDamage(collision.relativeVelocity.magnitude);
        }
    }
}
