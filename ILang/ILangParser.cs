using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Frontend.Tokens;
using System.Diagnostics;
using ILang.Mediator;
using ILang.Mediator.SymbolicTable;
using ILang.Frontend.Parsers;
using ILang.Mediator.Intermediate;

namespace ILang.Frontend
{
    public class ILangParser : Parser
    {
        public static readonly ISymbolTableStack Stack = SymbolicTableFactory.CreateStack();

        public ILangParser(Scanner scanner) : base(scanner) { }

        public ILangParser(ILangParser parser) : this(parser.Scanner) { }

        public override int ErrorCount
        {
            get
            {
                return ErrorHandler.Singleton.ErrorCount();
            }
        }

        public override void Parse()
        {
            ICode = IntermediateCodeFactory.CreateICode();
            Stopwatch.Start();
            Token token = NextToken();
            ICodeNode rootNode = null;

            if (token.Text == "method")
            {
                StatementParser parser = new StatementParser(this);
                rootNode = parser.Parse(token);

                token = CurrentToken(); //this should now be "end_of_file"
            }
            else
            {
                ErrorHandler.Singleton.Flag(token, "unexpected_token", this);
            }
            if (token.TokenType != "semicolon")
            {
                ErrorHandler.Singleton.Flag(token, "missing_end", this);
            }

            if (rootNode != null)
            {
                ICode.Root = rootNode;
            }

            Stopwatch.Stop();

            Parser.OnMessage(this, MessageType.ParserSummary, new Dictionary<string, object>
            {
                {"LineNumber", token.LineNumber},
                {"ErrorCount" , ErrorCount},
                {"Elapsed" , Stopwatch.Elapsed}
            });
        }

        public virtual ICodeNode Parse(Token token)
        {
            return null;
        }

        public void OnMessage(Token token)
        {
            Parser.OnMessage(this, MessageType.Token, new Dictionary<string, object>()
                    {
                        {"LineNumber", token.LineNumber},
                        {"Position", token.Position},
                        {"TokenType", token.TokenType},
                        {"TokenText", token.Text},
                        {"TokenValue", token.Value}
                    });

        }


        //while (!((token = NextToken()) is EndOfFileToken))
        //{
        //    ILangTokenType type = token.TokenType;
        //    if (type == "identifier")
        //    {
        //        OnMessage(token);
        //        String name = token.Text.ToLower();
        //        ISymbolTableEntry entry = Stack.Find(name);
        //        if (entry == null)
        //            entry = Stack.EnterLocal(name);
        //        entry.AppendLineNumber(token.LineNumber);
        //    }
        //    else if (ILangTokenType.IsSpecial(token.Text))
        //    {
        //        OnMessage(token);
        //    }
        //    else if (type == "error")
        //    {
        //        ErrorHandler.Singleton.Flag(token, token.Value != null ? token.Value.ToString() : "io_error", this);
        //    }
        //}
    }
}
