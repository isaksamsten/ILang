using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Frontend
{
    public struct ILangErrorCode
    {

        private string _value;
        private int _status;
        private string _type;

        public static implicit operator ILangErrorCode(string type)
        {
            return new ILangErrorCode(type);
        }

        private static Dictionary<string, string> ErrorCodes = new Dictionary<string, string>()
        {
            {"unknown", "Unknown error"},
            {"io_error", "Object I/O error"},
            {"too_many_errors", "Too many syntax errors"},
            {"invalid_character", "Invalid character"},
            {"unexpected_eof", "Unexpected end-of-file"},
            {"range_integer", "Integer literal out of range"},
            {"unexpected_token", "Unexpected token"},
            {"missing_end", "End is missing"},
            {"missing_colon_equals", "Missing assignment operator"},
            {"missing_semicolon", "Missing semicolon"}
        };

        public ILangErrorCode(string type)
        {
            if (ErrorCodes.ContainsKey(type))
            {
                _type = type;
                _value = ErrorCodes[type];
                _status = 0;
            }
            else
            {
                throw new ArgumentException(string.Format("{0} is not a valid error code", type));
            }
        }

        public int Status
        {
            get
            {
                return _status;
            }
        }

        public string Type
        {
            get { return _type; }
        }

        public string Value
        {
            get { return _value; }
        }

        public static bool operator ==(ILangErrorCode t1, string t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(ILangErrorCode t1, string t2)
        {
            return !(t1 == t2);
        }

        public static bool operator ==(ILangErrorCode t1, ILangErrorCode t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(ILangErrorCode t1, ILangErrorCode t2)
        {
            return !(t1 == t2);
        }

        public static bool operator ==(string t1, ILangErrorCode t2)
        {
            return t2.Equals(t1);
        }

        public static bool operator !=(string t1, ILangErrorCode t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            return Type.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
