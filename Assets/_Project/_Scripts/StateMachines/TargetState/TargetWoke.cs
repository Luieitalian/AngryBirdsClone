using UnityEngine;

namespace berkepite
{
    public class TargetWoke : TargetState
    {
        public override void EnterState(Target context)
        {

            context.Init();
            context.ChangeState(new TargetInitialising());
        }
        public override void UpdateState(Target context)
        {

        }
        public override void ExitState(Target context)
        {

        }
    }
}
