using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class Pig : MonoBehaviour
    {

        [SerializeField] private CHealth m_Health;
        [SerializeField] private float damageMultiplier;
        [SerializeField] private GameObject dieEffect;

        void Awake()
        {
            var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            m_Health = ScriptableObject.CreateInstance<CHealth>();
            m_Health.OnHealthDepleted(Die);
            m_Health.OnHealthDepleted(levelManager.OnPigDeath);
        }

        void Start()
        {
        }

        public void Die()
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity, transform.root);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Bird"))
                m_Health.TakeDamage(100);
            else
                m_Health.TakeDamage((int)(collision.relativeVelocity.magnitude * damageMultiplier));
        }
    }
}
