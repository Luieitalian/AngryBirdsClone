using UnityEngine;

namespace berkepite
{
    public class LevelManagerLost : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Entered LevelManagerLost state!");
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
            Debug.Log("Exited LevelManagerLost state!");
        }
    }
}
