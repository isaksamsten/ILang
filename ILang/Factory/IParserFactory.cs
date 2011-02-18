using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend.Factory
{
    public interface IParserFactory
    {
        Scanner GetScanner(Source source);
        Parser GetParser(Source source);
    }
}
