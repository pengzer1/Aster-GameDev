using UnityEngine;
using AG.GameLogic.BehaviorTree;

namespace AG.Pet.BehaviorTree
{
    public class CheckPlayerIsNear : Node
    {
        private static int playerLayerMask = 1 << 6;
        private Transform transform;
        private Animator anim;
        
        public CheckPlayerIsNear(Transform transform)
        {
            this.transform = transform;
            anim = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            var collider = Physics.OverlapSphere(transform.position, 5.0f, playerLayerMask);
            if(collider.Length <= 0)    return state = NodeState.Failure;

            anim.SetBool("Following", false);
            return state = NodeState.Success;
        }
    }
}