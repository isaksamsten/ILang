using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;

namespace ILang.Mediator
{
    public static class IntermediateCodeFactory
    {
        public static ICodeNode CreateICodeNode(ICodeNodeType type)
        {
            return new IntermediateCodeNode(type);
        }

        public static ICode CreateICode()
        {
            return new IntermediateCode();
        }
    }
}
