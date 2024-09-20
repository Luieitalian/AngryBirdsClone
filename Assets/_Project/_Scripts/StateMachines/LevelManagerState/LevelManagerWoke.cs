using UnityEngine;

namespace berkepite
{
    public class LevelManagerWoke : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Entered LevelManagerWoke state!");
            context.WakeUpTargets();
            context.ChangeState(new LevelManagerInitialising());
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
            Debug.Log("Exited LevelManagerWoke state!");
        }
    }
}
