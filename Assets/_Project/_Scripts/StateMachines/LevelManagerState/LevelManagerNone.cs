using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public class LevelManagerNone : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Entered LevelManagerNone state!");
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
            Debug.Log("Exited LevelManagerNone state!");
        }
    }
}
