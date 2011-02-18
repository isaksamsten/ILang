using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ILang.Frontend.Tokens;

namespace ILang.Frontend
{
    public abstract class Scanner
    {
        private Token _current;

        protected Source Source { get; set; }

        public virtual Token CurrentToken()
        {
            return _current;
        }

        public virtual Token NextToken()
        {
            _current = ExtractToken();
            return _current;
        }

        public virtual char CurrentChar()
        {
            return Source.CurrentChar();

        }

        public virtual char NextChar()
        {
            return Source.NextChar();
        }

        public Scanner(Source source)
        {
            Source = source;
        }

        protected abstract Token ExtractToken();


    }
}
