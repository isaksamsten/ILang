using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.Intermediate;
using ILang.Mediator;
using ILang.Mediator.SymbolicTable;

namespace ILang.Frontend.Parsers
{
    public class AssignmentParser : StatementParser
    {
        public AssignmentParser(ILangParser parser) : base(parser) { }

        public override Mediator.Intermediate.ICodeNode Parse(Tokens.Token token)
        {
            ICodeNode assignNode = IntermediateCodeFactory.CreateICodeNode("assign");
            string targetName = token.Text;
            ISymbolTableEntry symId = Stack.Find(targetName);
            if (symId == null)
                symId = Stack.EnterLocal(targetName);

            symId.AppendLineNumber(token.LineNumber);

            token = NextToken();
            ICodeNode variable = IntermediateCodeFactory.CreateICodeNode("variable");
            variable.SetAttribute("id", symId);

            assignNode.AddChild(variable);

            if (token.TokenType == "colon_equals")
            {
                token = NextToken(); //the other side i.e the thint that is the value
            }
            else
            {
                ErrorHandler.Singleton.Flag(token, "missing_colon_equals", this);
            }
            //simplify and asume constant value (int)
            ICodeNode constant = IntermediateCodeFactory.CreateICodeNode("int_const");
            constant.SetAttribute("value", token.Value);
            assignNode.AddChild(constant);

            NextToken(); //consume ;
            return assignNode;
        }
    }
}
