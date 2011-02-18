using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    public class ILangNumberToken : ILangToken
    {
        public ILangNumberToken(Source source) : base(source) { }

        protected override void Extract()
        {
            StringBuilder number = new StringBuilder();
            char curr = CurrentChar();
            while (char.IsDigit(curr))
            {
                number.Append(curr);
                curr = NextChar();
            }
            Text = number.ToString();
            TokenType = "integer";
            try
            {
                Value = int.Parse(Text);
            }
            catch
            {
                TokenType = "error";
                Value = "range_integer";
            }

        }
    }
}
