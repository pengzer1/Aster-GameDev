using UnityEngine;

namespace AG.Character
{
    public class CopyLimb : MonoBehaviour
    {
        [SerializeField]
        private Transform targetLimb;

        private ConfigurableJoint configurableJoint;

        private Quaternion targetInitialRotation;

        void Start()
        {
            configurableJoint = this.GetComponent<ConfigurableJoint>();
            targetInitialRotation = this.targetLimb.transform.localRotation;
        }

        private void FixedUpdate()
        {
            configurableJoint.targetRotation = CopyAnimRotation();
        }

        private Quaternion CopyAnimRotation()
        {
            return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
        }
    }
}