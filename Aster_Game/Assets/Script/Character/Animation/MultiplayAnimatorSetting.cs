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

        public void RequestAnimationTarget(GameObject player ,Action callback)
        {
            ProcessToTargetAnimation(player);
        }

        private void ProcessToTargetAnimation(GameObject player)
        {
            var anim = playerAnimatorToTarget[processIndex];

            var copyComponents = player.GetComponentsInChildren<CopyAnimToRagdoll>();
            foreach(var copyLimb in copyComponents)
            {
                var targetName = copyLimb.targetLimbName;
                var targetTransform = anim.GetTargetTransform(targetName);
                copyLimb.SetTargetLimb(targetTransform);
            }
        }
    }   
}