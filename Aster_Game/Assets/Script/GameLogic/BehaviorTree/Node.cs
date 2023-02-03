using System.Collections.Generic;

namespace AG.GameLogic.BehaviorTree
{
    public enum NodeState
    {
        Running,
        Failure,
        Success
    }

    public abstract class Node
    {
        protected NodeState state;

        public Node parentNode;

        private List<Node> childrenNode = new List<Node>();
        // TODO: rename
        private Dictionary<string, object> data = new Dictionary<string, object>();

        public Node()
        {
            parentNode = null;
        }

        public Node(List<Node> children)
        {
            foreach(var child in children)
            {
                AttatchChild(child);
            }
        }

        public void AttatchChild(Node child)
        {
            childrenNode.Add(child);
            child.parentNode = this;
        }

        public abstract NodeState Evaluate();

        public void SetData(string key, object value)
        {
            data[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (data.TryGetValue(key, out value))   return value;

            Node node = parentNode;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)  return value;
                node = node.parentNode;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                return true;
            }

            Node node = parentNode;
            while (node != null)
            {
                var cleared = node.ClearData(key);
                if (cleared)    return true;
                node = node.parentNode;
            }

            return false;
        }
    }
}