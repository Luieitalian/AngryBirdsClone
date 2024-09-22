using UnityEngine;

namespace berkepite
{
    public class Pig : MonoBehaviour
    {
        private TargetObject m_TargetObject;
        private AnimationClip initAnimation;

        void Awake()
        {
            m_TargetObject = GetComponent<TargetObject>();

            var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            m_TargetObject.Health.OnHealthDepleted(Die);
            m_TargetObject.Health.OnHealthDepleted(levelManager.OnPigDeath);

            m_TargetObject.AddAnim(CreateInitAnimationClip(), "initAnim");
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Bird"))
                m_TargetObject.Health.TakeDamage(100f);
            else
                m_TargetObject.Health.TakeDamage(collision.relativeVelocity.magnitude);
        }

        private AnimationClip CreateInitAnimationClip()
        {
            // Create a new animation clip
            initAnimation = new AnimationClip();
            initAnimation.legacy = true;

            float initialScale = transform.localScale.x;
            float modifiedScale = initialScale + .2f;

            // Create keyframes for the scale property
            Keyframe[] scaleX = new Keyframe[3];
            Keyframe[] scaleY = new Keyframe[3];
            Keyframe[] scaleZ = new Keyframe[3];

            // Set initial scale keyframe at time 0
            scaleX[0] = new Keyframe(0.0f, initialScale);
            scaleY[0] = new Keyframe(0.0f, initialScale);
            scaleZ[0] = new Keyframe(0.0f, initialScale);

            // Set modified scale keyframe at time 0.5
            scaleX[1] = new Keyframe(0.35f, modifiedScale);
            scaleY[1] = new Keyframe(0.35f, modifiedScale);
            scaleZ[1] = new Keyframe(0.35f, modifiedScale);

            // Set return to initial scale at time 1.0
            scaleX[2] = new Keyframe(0.7f, initialScale);
            scaleY[2] = new Keyframe(0.7f, initialScale);
            scaleZ[2] = new Keyframe(0.7f, initialScale);

            // Create animation curves for each axis
            AnimationCurve curveX = new AnimationCurve(scaleX);
            AnimationCurve curveY = new AnimationCurve(scaleY);
            AnimationCurve curveZ = new AnimationCurve(scaleZ);

            // Apply the curves to the initAnimation
            initAnimation.SetCurve("", typeof(Transform), "localScale.x", curveX);
            initAnimation.SetCurve("", typeof(Transform), "localScale.y", curveY);
            initAnimation.SetCurve("", typeof(Transform), "localScale.z", curveZ);

            return initAnimation;
        }
    }
}
