using UnityEngine;

namespace berkepite
{
    public class LevelManagerLost : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            context.OnLevelLost.Invoke();
            Debug.Log("Level Lost!");
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
        }
    }
}
