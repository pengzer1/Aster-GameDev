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
            switch (ragdollCharacterController.vertical)
            {
                case -1:
                    isForward = false;
                    isBackward = true;
                    break;
                case 0:
                    isForward = false;
                    isBackward = false;
                    break;
                case 1:
                    isForward = true;
                    isBackward = false;
                    break;
            }

            switch (ragdollCharacterController.horizontal)
            {
                case -1:
                    isLeft = true;
                    isRight = false;
                    break;
                case 0:
                    isLeft = false;
                    isRight = false;
                    break;
                case 1:
                    isLeft = false;
                    isRight = true;
                    break;
            }

            targetAnimator.SetBool("Forward", isForward);
            targetAnimator.SetBool("Backward", isBackward);
            targetAnimator.SetBool("Left", isLeft);
            targetAnimator.SetBool("Right", isRight);
        }
    }
}