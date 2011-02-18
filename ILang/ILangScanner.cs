using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Frontend.Tokens;

namespace ILang.Frontend
{
    class ILangScanner : Scanner
    {
        public ILangScanner(Source source) : base(source) { }

        protected override Token ExtractToken()
        {
            SkipWhitespace();
            Token token;
            char current = CurrentChar();
            if (current == Source.EOF)
            {
                token = new EndOfFileToken(Source);
            }
            else if (char.IsLetter(current))
            {
                token = new ILangWordToken(Source);
            }
            else if (char.IsDigit(current))
            {
                token = new ILangNumberToken(Source);
            }
            else if (current == '\'')
            {
                token = new ILangStringToken(Source);
            }
            else if (ILangTokenType.IsSpecial(current.ToString()))
            {
                token = new ILangSpecialSymbolToken(Source);
            }
            else
            {
                token = new ILangErrorToken(Source, "invalid_character", current);
                NextChar(); // only consume on error?
            }

            return token;
        }

        private void SkipWhitespace()
        {
            char cur = CurrentChar();
            while (char.IsWhiteSpace(cur) || cur == '"')
            {
                if (cur == '"') // comment?
                {
                    do
                    {
                        cur = NextChar();
                    } while (cur != '"' && cur != Source.EOF);
                    if (cur == '"')
                        cur = NextChar();
                }
                else
                {
                    cur = NextChar();
                }
            }
        }
    }
}
