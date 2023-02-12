using UnityEngine;

namespace AG.Character
{
    public class CopyAnimToRagdoll : MonoBehaviour
    {
        [SerializeField]
        private Transform targetLimb;
        private ConfigurableJoint configurableJoint;
        private Quaternion targetInitialRotation;

        void Start()
        {
            configurableJoint = GetComponent<ConfigurableJoint>();
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