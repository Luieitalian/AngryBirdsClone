using UnityEngine;

namespace berkepite
{
    public class GameBoundaries : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pig"))
            {
                Debug.Log("its pig!");
                other.GetComponent<Pig>().GetComponent<TargetObject>().Health.TakeDamage(1000);
            }
        }
    }

}