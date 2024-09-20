using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public abstract class SlingshotState
    {
        public abstract void EnterState(Slingshot context);
        public abstract void UpdateState(Slingshot context);
        public abstract void ExitState(Slingshot context);
    }
}
