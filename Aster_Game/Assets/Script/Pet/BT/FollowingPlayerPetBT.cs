using UnityEngine;
using System.Collections.Generic;
using AG.GameLogic.BehaviorTree;

namespace AG.Pet.BehaviorTree
{
    public class FollowingPlayerPetBT : AG.GameLogic.BehaviorTree.Tree
    {
        [SerializeField]
        private Transform pet;
        [SerializeField]
        private Transform player;

        protected override Node SetupBehaviorTree()
        {
            Node root = new SelectorNode(new List<Node>
            {
                new SelectorNode(new List<Node>
                {
                    new CheckPlayerDeadNode(pet, player),
                    new CheckPlayerWinNode(pet, player)
                }),
                new SequenceNode(new List<Node>
                {
                    new CheckPlayerIsNearNode(pet),
                    new StayNearPlayerNode(pet)
                }),
                new GoToPlayerNode(player, pet)
            });
            return root;
        }
    }
}