using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class TargetFinished : TargetState
    {
        public override void EnterState(Target context)
        {
            context.OnFinishedEvent.Invoke();
        }
        public override void UpdateState(Target context)
        {
        }
        public override void ExitState(Target context)
        {
        }
    }
}
