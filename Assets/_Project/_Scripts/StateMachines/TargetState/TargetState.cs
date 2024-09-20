using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public abstract class TargetState
    {
        public abstract void EnterState(Target context);
        public abstract void UpdateState(Target context);
        public abstract void ExitState(Target context);
    }
}
