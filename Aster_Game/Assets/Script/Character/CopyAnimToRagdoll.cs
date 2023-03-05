using UnityEngine;

namespace AG.PlayerComponent
{
    public class CopyAnimToRagdoll : MonoBehaviour
    {
        [SerializeField]
        private Transform targetLimb;
        public string targetLimbName;
        private bool isSet;
        private ConfigurableJoint configurableJoint;
        private Quaternion targetInitialRotation;

        void Start()
        {
            configurableJoint = GetComponent<ConfigurableJoint>();
            targetInitialRotation = this.targetLimb.transform.localRotation;
        }

        private void FixedUpdate()
        {
            if(!isSet)  return;
            configurableJoint.targetRotation = CopyAnimRotation();
        }

        private Quaternion CopyAnimRotation()
        {
            return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
        }

        public void SetTargetLimb(Transform target)
        {
            targetLimb = target;
            isSet = true;
        }
    }
}