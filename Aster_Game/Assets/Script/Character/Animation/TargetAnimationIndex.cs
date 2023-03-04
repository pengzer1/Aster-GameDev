using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG.PlayerComponent.Animation
{
    public class TargetAnimationIndex : MonoBehaviour
    {
        [SerializeField]
        private Transform[] limbsToBeTarget;
        public Transform GetTargetTransform(string limbName)
        {
            switch(limbName)
            {
                case "pelvis":
                    return limbsToBeTarget[0];
                case "spine_01":
                    return limbsToBeTarget[1];
                case "upperarm_l":
                    return limbsToBeTarget[2];
                case "lowerarm_l":
                    return limbsToBeTarget[3];
                case "hand_l":
                    return limbsToBeTarget[4];
                case "head":
                    return limbsToBeTarget[5];
                case "thigh_l":
                    return limbsToBeTarget[6];
                case "calf_l":
                    return limbsToBeTarget[7];
                case "thigh_r":
                    return limbsToBeTarget[8];
                case "calf_r":
                    return limbsToBeTarget[9];
                default:
                    return limbsToBeTarget[0];
            }
        }
    }
}