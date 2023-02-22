using UnityEngine;

namespace AG.PlayerComponent
{
    public class RagdollCharacterController : MonoBehaviour
    {

        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private ConfigurableJoint pelvisJoint;
        [SerializeField]
        private Rigidbody pelvis;
        private float horizontalInput;
        private float verticalInput;
        public float horizontal { get { return horizontalInput; } }
        public float vertical { get { return verticalInput; } }

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            CharcterMove();
        }

        private void CharcterMove()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
            direction.Normalize();
            pelvis.AddForce(direction * speed);
        }
    }
}