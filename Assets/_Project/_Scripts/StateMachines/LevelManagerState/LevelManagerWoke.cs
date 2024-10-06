using UnityEngine;

namespace berkepite
{
    public class LevelManagerWoke : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            context.WakeUpTargets();
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
        }
    }
}
