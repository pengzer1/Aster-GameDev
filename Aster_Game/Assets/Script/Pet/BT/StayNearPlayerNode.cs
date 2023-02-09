using UnityEngine;
using AG.GameLogic.BehaviorTree;

namespace AG.Pet.BehaviorTree
{
    public class StayNearPlayerNode : Node
    {
        private float stayTime = 1.0f;
        private float stayingTime = 0.0f;
        private Animator anim;

        public StayNearPlayerNode(Transform transform)
        {
            anim = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            anim.SetBool("Following", false);
            
            return state = NodeState.Running;
        }
    }
}