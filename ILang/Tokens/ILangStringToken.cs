using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    public class ILangStringToken : ILangToken
    {
        private static readonly char Quote = '\'';

        public ILangStringToken(Source source) : base(source) { }

        protected override void Extract()
        {
            StringBuilder value = new StringBuilder();

            char curr = NextChar();
            do
            {
                if (char.IsWhiteSpace(curr))
                    curr = ' ';
                if (curr != Quote && curr != Source.EOF)
                {
                    value.Append(curr);
                    curr = NextChar();
                }
                if (curr == Quote)
                {
                    while (curr == Quote && PeekChar() == Quote)
                    {
                        value.Append(curr);
                        curr = NextChar(); curr = NextChar();
                    }
                }

            } while (curr != '\'' && curr != Source.EOF);

            if (curr == Quote)
            {
                NextChar();
                TokenType = "string";
                Value = value.ToString();
            }
            else
            {
                TokenType = "error";
                Value = "unexpected_eof";
            }

            Text = "'" + value.Replace("'", "''").Append("'").ToString();
        }
    }
}
