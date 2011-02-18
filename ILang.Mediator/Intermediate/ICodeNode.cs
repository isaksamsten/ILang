using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.Intermediate
{
    public interface ICodeNode
    {
        ICodeNode Parent { get; }
        ICodeNodeType Type { get; }
        ICodeNode AddChild(ICodeNode node);

        IEnumerable<ICodeNode> Children { get; }

        void SetAttribute(ICodeKey key, object value);

        object GetAttribute(ICodeKey key);
    }
}
