using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;

namespace ILang.Mediator
{
    public class IntermediateCodeNode : Dictionary<ICodeKey, object>, ICodeNode, ICloneable
    {
        private ICodeNodeType _type = null;
        private ICodeNode _parent = null;
        private List<ICodeNode> _children;

        public IntermediateCodeNode(ICodeNodeType type)
        {
            _type = type;
            _parent = null;
            _children = new List<ICodeNode>();
        }

        public ICodeNode Parent
        {
            get { return _parent; }
        }

        public ICodeNodeType Type
        {
            get { return _type; }
        }

        public ICodeNode AddChild(ICodeNode node)
        {
            if (node != null)
            {
                (node as IntermediateCodeNode)._parent = this;
                _children.Add(node);
            }
            return node;
        }

        public IEnumerable<ICodeNode> Children
        {
            get { return _children; }
        }

        public void SetAttribute(ICodeKey key, object value)
        {
            Add(key, value);
        }

        public object GetAttribute(ICodeKey key)
        {
            object value;
            if (TryGetValue(key, out value))
                return value;
            return null;
        }

        public object Clone()
        {
            IntermediateCodeNode copy = (IntermediateCodeNode) IntermediateCodeFactory.CreateICodeNode(Type);

            foreach (var kv in this)
                copy.Add(kv.Key, kv.Value);

            return copy;
        }

        public override string ToString()
        {
            return Type.Text;
        }
    }
}
