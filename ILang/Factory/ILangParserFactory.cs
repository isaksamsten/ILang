using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Factory
{
    public class ILangParserFactory : IParserFactory
    {
        public Scanner GetScanner(Source source)
        {
            return new ILangScanner(source);
        }

        public Parser GetParser(Source source)
        {
            return new ILangParser(GetScanner(source));
        }
    }
}
