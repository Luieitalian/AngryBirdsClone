using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace berkepite
{
    [System.Serializable]
    public class CHealth
    {
        [SerializeField]
        private int m_MaxHealth = 100;
        private int m_CurrentHealth;

        public bool isHealthDepleted;
        public UnityEvent OnHealthDepletedEvent;

        public CHealth()
        {
            OnHealthDepletedEvent = new UnityEvent();
            m_CurrentHealth = m_MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            m_CurrentHealth -= damage;
            if (m_CurrentHealth <= 0)
            {
                isHealthDepleted = true;
                OnHealthDepletedEvent.Invoke();
            }
        }

        public void OnHealthDepleted(UnityAction callback)
        {
            OnHealthDepletedEvent.AddListener(callback);
        }

    }
}
