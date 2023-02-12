using UnityEngine;

namespace AG.RotateMouse
{
    public class RotateToMouse : MonoBehaviour
    {
        [SerializeField]
        private float rotCamXAxisSpeed = 4;
        [SerializeField]
        private float rotCamYAxisSpeed = 2;
        private float limitMinX = -80;
        private float limitMaxX = 50;
        private float eulerAngleX;
        private float eulerAngleY;

        public void UpdateRotate(float mouseX, float mouseY)
        {
            eulerAngleY += mouseX * rotCamYAxisSpeed;
            eulerAngleX += mouseY * rotCamXAxisSpeed;

            eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

            transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}

