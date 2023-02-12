using UnityEngine;
using System.Collections.Generic;
using AG.GameLogic.BehaviorTree;

namespace AG.Pet.BehaviorTree
{
    public class FollowingPlayerPetBT : AG.GameLogic.BehaviorTree.Tree
    {
        [SerializeField]
        private Transform player;
        [SerializeField]
        private Transform pet;

        protected override Node SetupBehaviorTree()
        {
            Node root = new SelectorNode(new List<Node>
            {
                new SequenceNode(new List<Node>
                {
                    new CheckPlayerIsNear(pet),
                    new StayNearPlayerNode(pet)
                }),
                new GoToPlayerNode(player, pet)
            });
            return root;
        }
    }
}