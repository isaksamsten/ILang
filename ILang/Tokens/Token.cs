using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Tokens
{
    public class Token
    {
        public ILangTokenType TokenType { get; protected set; }

        public string Text { get; protected set; }

        public object Value { get; protected set; }

        protected Source Source { get; set; }

        public int LineNumber { get; protected set; }

        public int Position { get; protected set; }

        public Token(Source source)
        {
            Source = source;
            LineNumber = Source.LineNumber;
            Position = Source.Position;
            TokenType = "error";
            Value = "unknown";
            Extract();
        }

        protected virtual void Extract()
        {
            Text = char.ToString(CurrentChar());

            NextChar();
        }

        protected virtual char CurrentChar()
        {
            return Source.CurrentChar();
        }

        protected virtual char NextChar()
        {
            return Source.NextChar();
        }

        protected virtual char PeekChar()
        {
            return Source.PeekChar();
        }


    }
}
