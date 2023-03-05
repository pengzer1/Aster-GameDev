using UnityEngine;
using Unity.Netcode;

namespace AG.PlayerComponent
{
    public class RagdollCharacterController : NetworkBehaviour
    {
        [SerializeField]
        private float speed = 3f;
        private float horizontalInput;
        private float verticalInput;
        public float horizontal { get { return horizontalInput; } }
        public float vertical { get { return verticalInput; } }

        private void Awake()
        {
            //if (!IsOwner) return;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            // if (!IsOwner) return;

            CharcterMove();
        }

        private void CharcterMove()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
            direction.Normalize();
            transform.position += direction * Time.deltaTime * speed;
        }
    }
}