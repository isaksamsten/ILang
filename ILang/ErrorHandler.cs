using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Frontend.Tokens;
using ILang.Mediator;

namespace ILang.Frontend
{
    public class ErrorHandler
    {
        public static ErrorHandler Singleton
        {
            get
            {
                if (_singleton == null)
                    _singleton = new ErrorHandler();
                return _singleton;
            }
        }
        public static readonly int MAX_ERROR = 10;
        private static int errors = 0;
        private static ErrorHandler _singleton;

        public void Flag(Token token, ILangErrorCode errorCode, Parser parser)
        {
            Parser.OnMessage(parser, MessageType.SyntaxError, new Dictionary<string, object>()
            {
                {"LineNumber", token.LineNumber}, 
                {"Position", token.Position}, 
                {"TokenText", token.Text}, 
                {"Error", errorCode}
            });

            if (++errors > MAX_ERROR)
                Fatal("too_many_errors", parser);

        }

        public void Fatal(ILangErrorCode errorCode, Parser parser)
        {
            Parser.OnMessage(parser, MessageType.SyntaxError, new Dictionary<string, object>()
            {
                {"LineNumber", 0}, 
                {"Position", 0}, 
                {"TokenText", ""}, 
                {"Error", "FATAL ERROR: " + errorCode}
            });

            Environment.Exit(errorCode.Status);
        }

        public int ErrorCount()
        {
            return errors;
        }
    }
}
