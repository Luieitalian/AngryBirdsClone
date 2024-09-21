using UnityEngine;

namespace berkepite
{
    public class SlingshotReloading : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            context.Reload();
        }
        public override void UpdateState(Slingshot context)
        {
        }
        public override void ExitState(Slingshot context)
        {
        }
    }
}
