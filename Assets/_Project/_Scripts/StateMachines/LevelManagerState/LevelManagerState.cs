using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public abstract class LevelManagerState
    {
        public abstract void EnterState(LevelManager context);
        public abstract void UpdateState(LevelManager context);
        public abstract void ExitState(LevelManager context);
    }
}
