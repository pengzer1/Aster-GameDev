using UnityEngine;
using AG.PlayerComponent.Animation;

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

        void Start()
        {
            targetAnimator = MultiplayAnimatorSetting.instance.RequestTargetAnimation(this.gameObject, null);
        }

        private void Update()
        {
            // TODO : 플레이어 속도 기준으로 변경
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