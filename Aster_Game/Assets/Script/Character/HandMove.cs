using UnityEngine;

namespace AG.HandSwing
{
    public class HandMove : MonoBehaviour
    {
        public GameObject hand;
        private float x1;
        private float x2;
        private float move;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                x1 = Input.mousePosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                x2 = Input.mousePosition.x;

                move = (x1 > x2) ? 1f : 2f;
            }

            if (move == 1f)
            {
                hand.transform.Translate(Vector2.left * (3 * Time.deltaTime));
            }

            if (move == 2f)
            {
                hand.transform.Translate(Vector2.right * (3 * Time.deltaTime));
            }
        }
    }
}