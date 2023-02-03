using System.Collections.Generic;

namespace AG.GameLogic.BehaviorTree
{
    public class SequenceNode : Node
    {
        public SequenceNode() : base() {}

        public SequenceNode(List<Node> children) : base(children) {}

        public override NodeState Evaluate()
        {
            NodeState nodeState;

            foreach (Node node in childrenNode)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        nodeState = NodeState.Success;
                        return nodeState;
                    case NodeState.Running:
                        nodeState = NodeState.Running;
                        return nodeState;
                    default:
                        continue;
                }
            }

            nodeState = NodeState.Failure;
            return nodeState;
        }
    }
}