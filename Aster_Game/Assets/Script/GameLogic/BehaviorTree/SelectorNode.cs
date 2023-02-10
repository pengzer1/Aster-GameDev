using System.Collections.Generic;

namespace AG.GameLogic.BehaviorTree
{
    public class SelectorNode : Node
    {
        public SelectorNode() : base(){}

        /// <summary> or node </summary>
        public SelectorNode(List<Node> children) : base(children){}

        public override NodeState Evaluate()
        {
            foreach(Node node in childrenNode)
            {
                switch(node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        return state = NodeState.Success;
                    case NodeState.Running:
                        return state = NodeState.Running;
                    default:
                        continue;
                }
            }

            return state = NodeState.Failure;
        }
    }   
}