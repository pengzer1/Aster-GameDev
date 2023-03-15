using UnityEngine;
using System;
using AG.PlayerComponent;

namespace AG.PlayerComponent.Animation
{
    public class MultiplayAnimatorSetting : MonoBehaviour
    {
        public static MultiplayAnimatorSetting instance { get; private set; }
        public TargetAnimationIndex[] playerAnimatorToTarget;
        [SerializeField]
        private int processIndex;

        private void Awake()
        {
            if(instance is null)
            {
                instance = this;
            }
        }

        public Animator RequestTargetAnimation(GameObject player, Action callback)
        {
            return ProcessAnimationTarget(player);
        }

        private Animator ProcessAnimationTarget(GameObject player)
        {
            var anim = playerAnimatorToTarget[processIndex];

            var copyComponents = player.GetComponentsInChildren<CopyAnimToRagdoll>();
            foreach(var copyLimb in copyComponents)
            {
                var targetName = copyLimb.targetLimbName;
                var targetTransform = anim.GetTargetTransform(targetName);
                copyLimb.SetTargetLimb(targetTransform);
            }
            var returnAnimator = playerAnimatorToTarget[processIndex].GetComponent<Animator>();
            processIndex++;
            return returnAnimator;
        }
    }   
}