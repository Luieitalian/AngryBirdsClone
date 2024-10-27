using UnityEngine;
using UnityEngine.UI;

namespace berkepite
{
    public class RemainingBirdsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _remainingBirdSlotPrefab;
        [SerializeField] private LevelManager _levelManager;

        void Awake()
        {

        }

        public void UpdateUI()
        {
            Destroy(transform.GetChild(0).gameObject);
            Debug.Log($"Updated Remaining Birds UI. Used {_levelManager.UsedBirdsCounter} bird(s).");
        }

        public void SetUI()
        {
            if (_levelManager.InitialBirds.Count == 0)
            {
                Debug.LogError("There is no remaining birds in 'remainingBirds' variable!\nPlease add birds!");
                Debug.Break();
            }

            foreach (var bird in _levelManager.InitialBirds)
            {
                var slot = Instantiate(_remainingBirdSlotPrefab, transform.position, Quaternion.identity, transform);
                slot.GetComponent<SpriteRenderer>().sprite = bird.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}
