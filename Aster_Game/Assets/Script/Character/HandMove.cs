using UnityEngine;

namespace AG.PlayerComponent
{
    public class HandMove : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;
        private float rotationYAngle;

        public void Update()
        {
            if (!Input.GetMouseButton(0))   return;

            rotationYAngle = Input.GetAxis("Mouse X") * speed;
            rotationYAngle = Mathf.Clamp(rotationYAngle, -10f, 10f);
            transform.Rotate(0f, rotationYAngle, 0f, Space.World);
        }
    }
}