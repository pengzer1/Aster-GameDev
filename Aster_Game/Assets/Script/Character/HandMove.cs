using UnityEngine;

namespace AG.PlayerComponent
{
    public class HandMove : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;
        private float rotationX;

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                rotationX = Input.GetAxis("Mouse X") * speed;
                rotationX = Mathf.Clamp(rotationX, -10f, 10f);
                transform.Rotate(0f, rotationX, 0f, Space.World);
            }
        }
    }
}