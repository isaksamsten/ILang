using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator;
using System.Diagnostics;
using ILang.Mediator.SymbolicTable;
using ILang.Mediator.Intermediate;

namespace ILang.Backend
{
    public class Executor
    {
        public static event EventHandler<MessageEventArgs> Message;

        protected ISymbolTable SymbolicTable { get; set; }
        protected ICode Code { get; set; }
        private Stopwatch _watch = new Stopwatch();
        protected Stopwatch Stopwatch { get { return _watch; } }

        public virtual void Process(ICode code, ISymbolTable symTab)
        {
            Stopwatch.Start();
            Stopwatch.Stop();
            int executionCount = 0;
            int runtimeError = 0;

            OnMessageEmitted(this, MessageType.InterpreterSummary, new Dictionary<string, object>()
            {
                {"ExecutionCount", executionCount},
                {"RuntimeError", runtimeError},
                {"Elapsed", Stopwatch.Elapsed}
            });

        }

        protected void OnMessageEmitted(object who, MessageType type, Dictionary<string, object> value)
        {
            if (Message != null)
                Message(who, new MessageEventArgs(type, value));
        }
    }
}
