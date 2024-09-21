using System;
using UnityEngine;

namespace berkepite
{
    [Serializable]
    public class CHealth
    {
        [SerializeField] private float m_MaxHealth;
        [SerializeField] private float damageMultiplier;

        public bool isHealthDepleted;
        public bool hasInvokedCriticalHealth;
        public Action HealthDepletedEvent;
        public Action CriticalHealthEvent;
        private float m_CurrentHealth;

        public void Init()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage * damageMultiplier;
            if (m_CurrentHealth <= 0)
            {
                isHealthDepleted = true;
                HealthDepletedEvent.Invoke();
            }
            else if (m_CurrentHealth <= m_MaxHealth / 2f && !hasInvokedCriticalHealth)
            {
                hasInvokedCriticalHealth = true;
                CriticalHealthEvent.Invoke();
            }
            else if (m_CurrentHealth > m_MaxHealth / 2f)
            {
                hasInvokedCriticalHealth = false;
            }
        }

        public void OnHealthDepleted(Action callback)
        {
            HealthDepletedEvent += callback;
        }

        public void OnCriticalHealth(Action callback)
        {
            CriticalHealthEvent += callback;
        }

    }
}
