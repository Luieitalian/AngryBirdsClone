using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace berkepite
{
    public abstract class BaseBird : MonoBehaviour
    {
        protected bool isLaunched = false;
        protected bool hasCollided = false;

        public abstract void Launch(Vector2 force);
    }
}
