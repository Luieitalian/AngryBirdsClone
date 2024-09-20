using UnityEngine;

namespace berkepite
{
    public class LevelManagerWon : LevelManagerState
    {
        public override void EnterState(LevelManager context)
        {
            Debug.Log("Entered LevelManagerWon state!");
            Debug.Log("Won!");
            context.SceneLoader.LoadScene(0);
        }
        public override void UpdateState(LevelManager context)
        {
        }
        public override void ExitState(LevelManager context)
        {
            Debug.Log("Exited LevelManagerWon state!");
        }
    }
}