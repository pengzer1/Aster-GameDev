using UnityEngine;
using AG.GameLogic.BehaviorTree;
using AG.PlayerComponent.Interfaces;

namespace AG.Pet.BehaviorTree
{
    public class CheckPlayerWinNode : Node
    {
        private Animator anim;
        private IDamageable player;

        public CheckPlayerWinNode(Transform transform, Transform player)
        {
            anim = transform.GetComponent<Animator>();
            this.player = player.GetComponent<IDamageable>();
        }
        
        public override NodeState Evaluate()
        {
            if(player.Win())
            {
                anim.SetBool("Win", true);
                return state = NodeState.Running;
            }

            return state = NodeState.Failure;
        }
    }
}