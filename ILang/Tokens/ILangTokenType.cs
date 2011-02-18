using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ILang.Frontend.Tokens
{
    public struct ILangTokenType
    {
        public static implicit operator ILangTokenType(string type)
        {
            return new ILangTokenType(type);
        }

        public ILangTokenType(string type)
        {
            if (ReservedWords.Contains(type) || TokenTypes.Contains(type))
            {
                _type = type;
                _value = null;
            }
            else if (SpecialSymbols.ContainsKey(type))
            {
                _type = SpecialSymbols[type];
                _value = type;
            }
            else
            {
                throw new ArgumentException(string.Format("{0} is not a valid ILangTokenType", type));
            }
        }

        public static bool IsReserved(string type)
        {
            return ReservedWords.Contains(type);
        }

        public static bool IsSpecial(string type)
        {
            return SpecialSymbols.ContainsKey(type);
        }

        public static readonly HashSet<string> TokenTypes = new HashSet<string>()
        {
            "identifier", "string", "error", "end_of_file", "integer", "float"
        };

        public static readonly HashSet<string> ReservedWords = new HashSet<string>()
        {
            "and", "array", "class", "case", "const", "div", "do", "else", "end",
            "elif", "for", "def", "if", "in", "label", "mod", "nil", "not", "of",
            "or", "then", "to", "until", "var", "while", "with", "true", "false",
            "method"
        };

        public static readonly Dictionary<string, string> SpecialSymbols = new Dictionary<string, string>() 
        {
             {"+", "plus"}, {"-", "minus"}, {"*", "star"}, {"/", "slash"}, {":=", "colon_equals"},
             {".", "dot"}, {",", "comma"}, {";", "semicolon"}, {":", "colon"}, {"'", "quote"}, {"=", "equal"},
             {"!=", "not_equal"}, {"<", "less_than"}, {"<=", "less_equal"}, {">=", "greater_equal"}, {">", "greater"}, 
             {"(", "left_paren"}, {")", "right_paren"}, {"[", "left_bracket"}, {"]", "right_bracket"}, {"{", "left_brace"},
             {"}", "right_brace"}, {"^", "up_arrow"}, {"..", "dot_dot"}, {"::=", "is_a"}
        };

        private string _type;
        public string Type
        {
            get { return _type; }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
        }

        public static bool operator ==(ILangTokenType t1, string t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(ILangTokenType t1, string t2)
        {
            return !(t1 == t2);
        }

        public static bool operator ==(ILangTokenType t1, ILangTokenType t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(ILangTokenType t1, ILangTokenType t2)
        {
            return !t1.Equals(t2);
        }

        public static bool operator ==(string t1, ILangTokenType t2)
        {
            return t2.Equals(t1);
        }

        public static bool operator !=(string t1, ILangTokenType t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            return Type.Equals(obj.ToString());
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
