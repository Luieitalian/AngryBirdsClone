using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class SlingshotIdle : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            Debug.Log("Entered SlingshotIdle state!");
        }
        public override void UpdateState(Slingshot context)
        {
            if (context.TouchAction.IsPressed() && context.IsWithinSlingshotArea())
                context.ChangeState(new SlingshotHolding());
        }
        public override void ExitState(Slingshot context)
        {
            Debug.Log("Exited SlingshotIdle state!");
        }
    }
}
