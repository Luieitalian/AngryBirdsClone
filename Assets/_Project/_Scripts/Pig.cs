using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class Pig : MonoBehaviour
    {
        [SerializeField]
        private CHealth m_Health;

        [SerializeField]
        private float damageMultiplier;

        void Awake()
        {
            m_Health = new CHealth();
            m_Health.OnHealthDepleted(Die);
        }

        void Start()
        {
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Bird")) Die();
            m_Health.TakeDamage((int)(collision.relativeVelocity.magnitude * damageMultiplier));
        }
    }
}
