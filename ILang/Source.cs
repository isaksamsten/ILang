using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ILang.Mediator;

namespace ILang.Frontend
{
    public class Source : IDisposable
    {

        public static event EventHandler<MessageEventArgs> Message;

        public const int InitialPosition = -2;
        public const int StartPosition = -1;
        public const char EOL = '\n';
        public const char EOF = '\0';

        protected TextReader Reader { get; private set; }

        protected string Line { get; set; }
        public int LineNumber { get; set; }
        public int Position { get; set; }

        public Source(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            LineNumber = 0;
            Position = InitialPosition;
            Reader = reader;
        }

        /// <summary>
        /// Return the next char in the source
        /// </summary>
        /// <returns></returns>
        public char NextChar()
        {
            ++Position;
            return CurrentChar();
        }

        /// <summary>
        /// Return the current char in the Source
        /// </summary>
        /// <returns></returns>
        public char CurrentChar()
        {
            if (Position == -2)
            {
                ReadLine();
                return NextChar();
            }
            else if (Line == null)
            {
                return EOF;
            }
            else if (Position == -1 || Position == Line.Length)
            {
                return EOL;
            }
            else if (Position > Line.Length)
            {
                ReadLine();
                return NextChar();
            }
            else
            {
                return Line[Position];
            }
        }

        /// <summary>
        /// Return next char; but dont adwance CurrentChar
        /// </summary>
        /// <returns>Next char</returns>
        public char PeekChar()
        {
            CurrentChar();
            return Line == null ? EOF : Position + 1 < Line.Length ? Line[Position + 1] : EOL;
        }

        private void ReadLine()
        {
            Line = Reader.ReadLine();
            Position = StartPosition;
            if (Line != null)
            {
                ++LineNumber;
                OnMessageEmitted(this, MessageType.SourceLine, new Dictionary<string, object>()
                { 
                    { "LineNumber", LineNumber },
                    { "LineText", Line }
                });
            }
        }

        public static void OnMessageEmitted(object who, MessageType type, Dictionary<string, object> message)
        {
            if (Message != null)
                Message(who, new MessageEventArgs(type, message));
        }

        public void Dispose()
        {
            Reader.Close();
        }
    }
}
