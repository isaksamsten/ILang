using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Frontend;
using ILang.Mediator;
using ILang.Backend;
using System.IO;
using ILang.Frontend.Factory;
using ILang.Frontend.Tokens;
using ILang.Mediator.Intermediate;
using ILang.Mediator.SymbolicTable;

namespace ILang.Runtime
{
    public class ILang
    {
        private Parser _parser;
        private Source _source;
        private ICode _iCode;
        private ISymbolTable _symTab;
        private Executor _executor;

        public ILang(string file, string flags)
        {
            flags = "-x -i";
            bool xref = flags.Contains("-x");
            bool inter = flags.Contains("-i");
            try
            {
                using (_source = new Source(new StreamReader(file)))
                {
                    Source.Message += new EventHandler<MessageEventArgs>(MessageEmitted);
                    Parser.Message += new EventHandler<MessageEventArgs>(MessageEmitted);

                    _parser = new ILangParserFactory().GetParser(_source);
                    _parser.Parse();

                    _iCode = _parser.ICode;
                    _symTab = _parser.SymbolicTable;

                    _executor = new Executor();
                    _executor.Process(_iCode, _symTab);
                }
                if (xref)
                {
                    CrossReferencer.Print(ILangParser.Stack);
                }
                if (inter)
                {
                    Console.WriteLine();
                    new SyntaxTreePrinter(Console.Out).Print(_iCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("******* Internal error. *******");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

        }

        void MessageEmitted(object sender, MessageEventArgs e)
        {
            MessageType type = e.Type;
            Dictionary<string, object> message = e.Message;

            switch (type)
            {
                case MessageType.ParserSummary:
                    Console.WriteLine(ParserSummaryFormat,
                        message["LineNumber"],
                        message["ErrorCount"],
                        (message["Elapsed"] as TimeSpan?).Value.TotalSeconds);
                    break;
                case MessageType.SyntaxError:
                    StringBuilder flag = new StringBuilder();
                    for (int i = 1; i < PrefixWidth + (int)message["Position"]; ++i)
                        flag.Append(" ");
                    flag.Append("^\n*** ").Append(message["Error"]);
                    if (message["TokenText"] != null)
                        flag.Append(@" [at """).Append(message["TokenText"]).Append("\"]\n");
                    Console.WriteLine(flag.ToString());
                    break;
                case MessageType.Token:
                    //Console.WriteLine(TokenFormat, message["TokenType"], message["LineNumber"], message["Position"], message["TokenText"]);
                    //if (message["TokenValue"] != "unknown")
                    //    Console.WriteLine(ValueFormat, message["TokenValue"]);
                    break;
                case MessageType.SourceLine:
                    Console.WriteLine(SourceLineFormat,
                        message["LineNumber"],
                        message["LineText"]);
                    break;
            }

        }

        public static void Main(string[] args)
        {
            string file = @"C:\Users\isak\Documents\Visual Studio 2010\Projects\ILang\ILang.Runtime\test.il";
            string flags = "";
            new ILang(file, flags);
            Console.ReadKey();
        }

        private static readonly string ParserSummaryFormat =
            "\n\t{0} source lines." +
            "\n\t{1} syntax error." +
            "\n\t{2} seconds total parsing time.\n";

        private static readonly string SourceLineFormat = "{0:D3} {1}";

        private static readonly string TokenFormat = ">>> {0} line={1}, pos={2}, text=\"{3}\"";

        private static readonly string ValueFormat = ">>>               value={0}";

        private static readonly int PrefixWidth = 5;
    }
}
