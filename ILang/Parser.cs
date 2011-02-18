using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ILang.Frontend.Tokens;
using ILang.Mediator;
using System.Diagnostics;
using ILang.Mediator.Intermediate;
using ILang.Mediator.SymbolicTable;
namespace ILang.Frontend
{

    public abstract class Parser
    {

        public static event EventHandler<MessageEventArgs> Message;

        private Stopwatch _stopWatch = new Stopwatch();

        protected Scanner Scanner { get; private set; }

        public abstract int ErrorCount { get; }

        public virtual Token CurrentToken()
        {
            return Scanner.CurrentToken();
        }

        public virtual Token NextToken()
        {
            return Scanner.NextToken();
        }

        protected Stopwatch Stopwatch { get { return _stopWatch; } }

        public ICode Code { get; protected set; }

        /// <summary>
        /// Construct a parser using a scanner
        /// </summary>
        /// <param name="scanner"></param>
        protected Parser(Scanner scanner)
        {
            Scanner = scanner;
        }

        public abstract void Parse();

        public static void OnMessage(object who, MessageType type, Dictionary<string, object> message)
        {
            if (Message != null)
                Message(who, new MessageEventArgs(type, message));
        }

        public ICode ICode { get; protected set; }

        public ISymbolTable SymbolicTable { get; protected set; }
    }
}
