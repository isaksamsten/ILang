using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.SymbolicTable;

namespace ILang.Mediator
{
    public class ILangSymbolTable : SortedDictionary<string, ISymbolTableEntry>, ISymbolTable
    {
        private int _level;

        public ILangSymbolTable(int level)
        {
            this._level = level;
        }

        public int NestingLevel
        {
            get { return _level; }
        }

        public ISymbolTableEntry Enter(string name)
        {
            ISymbolTableEntry entry = SymbolicTableFactory.CreateEntry(name, this);
            Add(name, entry);

            return entry;
        }

        public ISymbolTableEntry Find(string name)
        {
            ISymbolTableEntry entry;
            if (TryGetValue(name, out entry))
                return entry;
            return null;
        }

        public IEnumerable<ISymbolTableEntry> SortedEntries
        {
            get { return Values.AsEnumerable(); }
        }
    }
}
