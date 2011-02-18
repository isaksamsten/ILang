using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    class ILangWordToken : ILangToken
    {
        public ILangWordToken(Source source) : base(source) { }

        protected override void Extract()
        {
            StringBuilder textBuff = new StringBuilder();
            char current = CurrentChar();

            while (char.IsLetterOrDigit(current))
            {
                textBuff.Append(current);
                current = NextChar();
            }

            Text = textBuff.ToString();
            TokenType = ILangTokenType.IsReserved(Text) ? Text : "identifier";
        }
    }
}
