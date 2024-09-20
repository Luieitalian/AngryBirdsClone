using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class SlingshotHolding : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            Debug.Log("Entered SlingshotHolding state!");
        }
        public override void UpdateState(Slingshot context)
        {
            if (context.TouchAction.IsPressed())
                context.HandleHolding();
            else
                context.HandleReleased();
        }
        public override void ExitState(Slingshot context)
        {
            Debug.Log("Exited SlingshotHolding state!");
        }
    }
}
