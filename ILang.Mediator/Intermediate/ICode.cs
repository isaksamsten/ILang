using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.Intermediate
{
    public interface ICode
    {
        ICodeNode Root { get; set; }
    }
}
