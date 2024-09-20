using UnityEngine;

namespace berkepite
{
    public class LevelManagerPlaying : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Entered LevelManagerPlaying state!");
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
            Debug.Log("Exited LevelManagerPlaying state!");
        }
    }
}
