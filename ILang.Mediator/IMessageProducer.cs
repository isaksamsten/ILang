using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator
{
    [Flags]
    public enum MessageType
    {
        /// <summary>
        ///    new
        ///    {
        ///       LineNumber = LineNumber,
        ///       LineText = Line
        ///   }
        /// </summary>
        SourceLine,
        SyntaxError,

        /// <summary>
        /// Return                        
        /// new { int LineNumber, int ErrorCount, TimeSpan Elapsed }
        /// </summary>
        ParserSummary,

        /// <summary>
        /// new { int ExecutionCount, int RuntimeError, TimeSpan Elapsed }
        /// </summary>
        InterpreterSummary,
        CompilerSummary,
        Misc,
        Token,
        Assign,
        Fetch,
        BreakPoint,
        RuntimeError,
        Call,
        Return
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(MessageType type, Dictionary<string, object> message)
        {
            Type = type;
            Message = message;
        }

        public MessageType Type { get; private set; }
        public Dictionary<string, object> Message { get; private set; }
    }

    public interface IMessageProducer
    {
        /// <summary>
        /// Emmited when there are a message to display
        /// </summary>
        event EventHandler<MessageEventArgs> Message;
    }
}
