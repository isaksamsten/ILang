using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.SymbolicTable;

namespace ILang.Mediator
{
    public class ILangSymbolTableEntry : Dictionary<ISymbolTableKey, object>, ISymbolTableEntry
    {
        private string _name;
        private ISymbolTable _table;
        private List<int> _lineNumbers = new List<int>();

        public ILangSymbolTableEntry(string name, ISymbolTable table)
        {
            _name = name;
            _table = table;
        }

        public string Name
        {
            get { return _name; }
        }

        public ISymbolTable SymbolicTable
        {
            get { return _table; }
        }

        public void AppendLineNumber(int line)
        {
            _lineNumbers.Add(line);
        }

        public IEnumerable<int> LineNumbers
        {
            get { return _lineNumbers.ToArray(); }
        }

        public void SetAttribute(ISymbolTableKey key, object value)
        {
            Add(key, value);
        }

        public object GetAttribute(ISymbolTableKey key)
        {
            object value;
            if (TryGetValue(key, out value))
                return value;

            return value;
        }

        public override string ToString()
        {
            return _name + " at [ " + string.Concat(LineNumbers.Select(x => x.ToString() + " ")) + "]";
        }
    }
}
