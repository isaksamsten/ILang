using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.Intermediate
{
    public class ICodeNodeType
    {
        public static implicit operator ICodeNodeType(string str)
        {
            return new ICodeNodeType(str);
        }
        private static HashSet<string> Allowed = new HashSet<string>()
        {
            "class", "method",

            "assign", "loop", "with", "call", "arguments",
            "if", "select", "select_branch", "select_constants",
            "no_op",

            "equal", "not_equal", "less_than", "less_equal", 
            "greater_than", "greater_equal", "not",

            "add", "sub", "or",

            "multiply", "int_div", "float_div", "mod", "and", "but",

            "variable", "field", "int_const", "float_const",
            "string_const", "bool_const"


        };

        public ICodeNodeType(string str)
        {
            if (Allowed.Contains(str))
                Text = str;
            else
                throw new ArgumentException(str + " is not an allowed ICodeNodeType");
        }

        public string Text { get; private set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
