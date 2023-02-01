using UnityEngine;

namespace AG.CharacterController
{
    public class RagdollCharacterController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private ConfigurableJoint pelvisJoint;

        [SerializeField]
        private Rigidbody pelvis;

        [SerializeField]
        private Animator targetAnimator;

        private bool walk = false;

        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.sqrMagnitude >= 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                pelvisJoint.targetRotation = Quaternion.Euler(targetAngle, 0f, 0f);

                pelvis.AddForce(direction * speed);

                walk = true;
            }
            else
            {
                walk = false;
            }

            targetAnimator.SetBool("Walk", this.walk);
        }
    }
}