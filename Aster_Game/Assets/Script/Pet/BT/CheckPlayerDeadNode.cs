using UnityEngine;
using AG.GameLogic.BehaviorTree;
using AG.PlayerComponent.Interfaces;

namespace AG.Pet.BehaviorTree
{
    public class CheckPlayerDeadNode : Node
    {
        private Animator anim;
        private IDamageable player;

        public CheckPlayerDeadNode(Transform transform, Transform player)
        {
            anim = transform.GetComponent<Animator>();
            this.player = player.GetComponent<IDamageable>();
        }
        
        public override NodeState Evaluate()
        {
            if(player.Dead())
            {
                anim.SetBool("Dead", true);
                return state = NodeState.Running;
            }

            return state = NodeState.Failure;
        }
    }
}