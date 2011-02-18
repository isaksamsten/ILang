using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    public class ILangSpecialSymbolToken : ILangToken
    {
        private static HashSet<char> Singles = new HashSet<char>()
        {
            '+','-','*','/',',',';','\\','=','{','}','[',']','(',')','^'
        };

        public ILangSpecialSymbolToken(Source source) : base(source) { }

        protected override void Extract()
        {
            char curr = CurrentChar();
            Text = curr.ToString();

            if (Singles.Contains(curr))
            {
                NextChar(); // consume current.
            }
            else // it's not a single, its a double (or triple) or invalid
            {
                char next = NextChar();
                switch (curr)
                {
                    case ':':
                        char peek = PeekChar();
                        if (next == '=') // assign
                        {
                            Text += next;
                            NextChar(); // consume = 
                        }
                        else if (next == ':' && peek == '=') //is_a 
                        {
                            Text += next + "" + NextChar(); // append and consume :
                            NextChar(); //consume =
                        }
                        break;
                    case '<':
                        if (next == '=' || next == '>') // greater_equal or not_equal
                        {
                            Text += next;
                            NextChar(); //consume = or >
                        }
                        break;
                    case '>':
                        if (next == '=') // less_equal
                        {
                            Text += next;
                            NextChar(); // consume =
                        }
                        break;
                    case '.':
                        if (next == '.') //dot_dot
                        {
                            Text += next;
                            NextChar(); // consume .
                        }
                        break;
                    default: //something else
                        TokenType = "error";
                        Value = "invalid_character";
                        NextChar(); // consume bad char
                        break;
                }
            }
            if (TokenType == "error")
                TokenType = Text;
        }
    }
}
