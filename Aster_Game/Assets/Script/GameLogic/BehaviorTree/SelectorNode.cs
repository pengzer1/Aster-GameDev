using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG.GameLogic.BehaviorTree
{
    public class SelectorNode : Node
    {
        public SelectorNode() : base(){}

        public SelectorNode(List<Node> children) : base(children){}

        public override NodeState Evaluate()
        {
            NodeState nodeState;
            bool anyChildIsRunning = false;

            foreach (Node node in childrenNode)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        nodeState = NodeState.Failure;
                        return nodeState;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        nodeState = NodeState.Success;
                        return nodeState;
                }
            }

            nodeState = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return nodeState;
        }
    }   
}