using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.Intermediate
{
    public class ICodeKey
    {
        public static implicit operator ICodeKey(string s)
        {
            return new ICodeKey(s);
        }

        private static HashSet<string> Keys = new HashSet<string>() 
        {
            "line", "id", "value"
        };

        public ICodeKey(string s)
        {
            if (Keys.Contains(s))
                Text = s;
            else
                throw new ArgumentException(s + " is not a valid ICodeKey");
        }

        public string Text { get; private set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
