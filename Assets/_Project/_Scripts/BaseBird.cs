using UnityEngine;
using UnityEngine.InputSystem;

namespace berkepite
{
    public abstract class BaseBird : MonoBehaviour
    {
        protected bool isLaunched = false;
        protected bool hasCollided = false;
        protected bool hasUsedPower = false;

        protected Controls controls;
        protected InputAction birdAbilityTouchAction;

        public abstract float LaunchPower { get; protected set; }

        public abstract void Launch(Vector2 force);
        public abstract void UseAbility();
    }
}
