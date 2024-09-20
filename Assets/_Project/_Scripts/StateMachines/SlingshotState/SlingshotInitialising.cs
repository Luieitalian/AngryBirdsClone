using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class SlingshotInitialising : SlingshotState
    {
        public override void EnterState(Slingshot context)
        {
            Debug.Log("Entered SlingshotInitialising state!");
        }
        public override void UpdateState(Slingshot context)
        {
        }
        public override void ExitState(Slingshot context)
        {
            Debug.Log("Exited SlingshotInitialising state!");
        }
    }
}
