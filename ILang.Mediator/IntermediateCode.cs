using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;

namespace ILang.Mediator
{
    public class IntermediateCode : ICode
    {
        private ICodeNode _root = null;
        public ICodeNode Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }
    }
}
