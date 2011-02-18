using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Frontend.Tokens;

namespace ILang.Frontend.Tokens
{
    public class ILangErrorToken : ILangToken
    {
        public ILangErrorToken(Source source, ILangErrorCode error, char current)
            : base(source)
        {
            TokenType = "error";
            Value = error;
            Text = current.ToString();
        }

        protected override void Extract()
        {
            
        }
    }
}
