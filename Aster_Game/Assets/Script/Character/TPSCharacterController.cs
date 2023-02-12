using UnityEngine;

namespace AG.PlayerTPSController
{
    public class TPSCharacterController : MonoBehaviour
    {
        [SerializeField]
        private Transform charactorBody;
        [SerializeField]
        private Transform cameraArm;
        [SerializeField]
        private Animator targetAnimator;

        void Update()
        {
            LookAround();
            Move();
        }

        private void Move()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isWorking = moveInput.magnitude != 0;
            targetAnimator.SetBool("Walk", isWorking);

            if (isWorking)
            {
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                charactorBody.forward = lookForward;
                transform.position += moveDir * Time.deltaTime * 5f;
            }
        }

        private void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 camAngle = cameraArm.rotation.eulerAngles;

            float x = camAngle.x - mouseDelta.y;

            x = (x < 180f) ? Mathf.Clamp(x, -1f, 50f) : Mathf.Clamp(x, 335f, 361);

            cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        }
    }
}