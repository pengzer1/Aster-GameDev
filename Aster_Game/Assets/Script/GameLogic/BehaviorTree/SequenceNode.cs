using System.Collections.Generic;

namespace AG.GameLogic.BehaviorTree
{
    public class SequenceNode : Node
    {
        public SequenceNode() : base() {}

        /// <summary> and node </summary>
        public SequenceNode(List<Node> children) : base(children) {}

        public override NodeState Evaluate()
        {
            bool bNowRunning = false;
            foreach (Node node in childrenNode)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        return state = NodeState.Failure;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        bNowRunning = true;
                        continue;
                    default:
                        continue;
                }
            }

            return state = bNowRunning ? NodeState.Running : NodeState.Success;
        }
    }
}