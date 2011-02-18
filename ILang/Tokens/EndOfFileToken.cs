using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    public class EndOfFileToken : Token
    {
        public EndOfFileToken(Source source) : base(source) { }

        protected override void Extract()
        {

        }
    }
}
