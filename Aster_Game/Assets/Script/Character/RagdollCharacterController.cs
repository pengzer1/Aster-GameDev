using UnityEngine;
using AG.RotateMouse;

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
        [SerializeField]
        private Transform cameraTransform;
        private bool isWalking;
        private RotateToMouse rotateMouse;

        void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

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

                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            targetAnimator.SetBool("Walk", isWalking);

            direction = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * direction;
        }
    }
}