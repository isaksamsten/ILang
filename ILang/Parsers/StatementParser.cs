using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;
using ILang.Frontend.Tokens;
using ILang.Mediator;

namespace ILang.Frontend.Parsers
{
    public class StatementParser : ILangParser
    {
        public StatementParser(ILangParser parent) : base(parent) { }

        public override ICodeNode Parse(Token token)
        {
            ICodeNode node = null;
            switch (token.TokenType.ToString())
            {
                case "method":
                    MethodParser methodParser = new MethodParser(this);
                    node = methodParser.Parse(token);
                    // CurrentToken() == ";"
                    break;
                case "identifier":
                    AssignmentParser assignmentParser = new AssignmentParser(this);
                    node = assignmentParser.Parse(token);
                    break;
                default:
                    node = IntermediateCodeFactory.CreateICodeNode("no_op");
                    break;
            }
            if (node != null)
                node.SetAttribute("line", token.LineNumber);

            return node;
        }

        public void ParseList(Token token, ref ICodeNode parent, ILangTokenType terminator, ILangErrorCode error)
        {
            // As long as we are parseing neither endToken or terminating token
            // do this
            while (!(token is EndOfFileToken) && (token.TokenType.ToString() != terminator.ToString()))
            {
                ICodeNode statementNode = Parse(token);
                parent.AddChild(statementNode);

                // token have been moved; get the latest
                token = CurrentToken();

                if (token.TokenType == "semicolon")
                {
                    token = NextToken(); // move beyond the statement separator.
                }
                else if (token.TokenType == "identifier")
                {
                    ErrorHandler.Singleton.Flag(token, "missing_semicolon", this);
                }
                else if (token.TokenType != terminator)
                {
                    ErrorHandler.Singleton.Flag(token, "unexpected_token", this);
                    token = NextToken();
                }
            }

            if (token.TokenType == terminator)
            {
                token = NextToken();
            }
            else
            {
                ErrorHandler.Singleton.Flag(token, error, this);
            }
        }
    }
}
