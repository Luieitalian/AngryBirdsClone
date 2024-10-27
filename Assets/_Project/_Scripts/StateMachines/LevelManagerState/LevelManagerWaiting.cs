using UnityEngine;
using System.Collections;

namespace berkepite
{
    public class LevelManagerWaiting : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Level waiting for pigs to die...");
            context.StartCoroutine(WaitForPigsToDie(5f, context));
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
        }

        private IEnumerator WaitForPigsToDie(float time, LevelManager context)
        {
            yield return new WaitForSeconds(time);
            if (context.PigCount == 0)
                context.ChangeState(new LevelManagerWon());
            else
                context.ChangeState(new LevelManagerLost());
        }
    }
}
