using UnityEngine;
using AG.GameLogic.BehaviorTree;

namespace AG.Pet.BehaviorTree
{
    public class GoToPlayerNode : Node
    {
        private Transform player;
        private Transform transform;
        private Animator anim;

        public GoToPlayerNode(Transform player, Transform transform)
        {
            this.player = player;
            this.transform = transform;
            anim = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            transform.LookAt(player);
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime);
            anim.SetBool("Following", true);

            return state = NodeState.Running;
        }
    }
}