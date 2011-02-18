using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;
using ILang.Frontend.Tokens;
using ILang.Mediator;

namespace ILang.Frontend.Parsers
{
    public class MethodParser : StatementParser
    {
        public MethodParser(ILangParser parser) : base(parser) { }

        public override ICodeNode Parse(Token token)
        {
            token = NextToken(); // after method
            ICodeNode methodNode = IntermediateCodeFactory.CreateICodeNode("method");

            StatementParser statementParser = new StatementParser(this);
            statementParser.ParseList(token, ref methodNode, "end", "missing_end");
            token = CurrentToken();
            return methodNode;
        }
    }
}
