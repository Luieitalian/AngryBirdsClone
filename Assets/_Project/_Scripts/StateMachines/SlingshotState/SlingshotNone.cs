using UnityEngine;

namespace berkepite
{
    public class SlingshotNone : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            Debug.Log("Entered SlingshotNone state!");
        }
        public override void UpdateState(Slingshot context)
        {
        }
        public override void ExitState(Slingshot context)
        {
            Debug.Log("Exited SlingshotNone state!");
        }
    }
}
