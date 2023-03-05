using UnityEngine;

namespace AG.PlayerComponent
{
    public class CharacterAnim : MonoBehaviour
    {
        [SerializeField]
        private Animator targetAnimator;
        [SerializeField]
        private RagdollCharacterController ragdollCharacterController;
        private bool isForward;
        private bool isBackward;
        private bool isLeft;
        private bool isRight;

        private void Update()
        {
            isForward = ragdollCharacterController.vertical >= 1;
            isBackward = ragdollCharacterController.vertical <= -1; 
            isLeft = ragdollCharacterController.horizontal <= -1;
            isRight = ragdollCharacterController.horizontal >= 1;

            targetAnimator.SetBool("Forward", isForward);
            targetAnimator.SetBool("Backward", isBackward);
            targetAnimator.SetBool("Left", isLeft);
            targetAnimator.SetBool("Right", isRight);
        }
    }
}