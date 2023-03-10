using UnityEngine;
using System;
using AG.PlayerComponent;

namespace AG.PlayerComponent.Animation
{
    public class MultiplayAnimatorSetting : MonoBehaviour
    {
        public static MultiplayAnimatorSetting instance { get; private set; }
        public TargetAnimationIndex[] playerAnimatorToTarget;
        private int processIndex;

        private void Awake()
        {
            if(instance is not null)
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
                Debug.Log($"zz1");
                var targetName = copyLimb.targetLimbName;
                var targetTransform = anim.GetTargetTransform(targetName);
                copyLimb.SetTargetLimb(targetTransform);
            }
            return playerAnimatorToTarget[processIndex].GetComponent<Animator>();
        }
    }   
}