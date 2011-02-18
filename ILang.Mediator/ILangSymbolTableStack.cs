using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.SymbolicTable;

namespace ILang.Mediator
{
    public class ILangSymbolTableStack : List<ISymbolTable>, ISymbolTableStack
    {
        private int _currentNestingLevel = 0;

        public ILangSymbolTableStack()
        {
            Add(SymbolicTableFactory.CreateTable(CurrentNestingLevel));
        }

        public int CurrentNestingLevel
        {
            get { return _currentNestingLevel; }
        }

        public ISymbolTable Local
        {
            get { return this[CurrentNestingLevel]; }
        }

        public ISymbolTableEntry EnterLocal(string name)
        {
            return this[CurrentNestingLevel].Enter(name);
        }

        public ISymbolTableEntry FindLocal(string name)
        {
            return this[CurrentNestingLevel].Find(name);
        }

        public ISymbolTableEntry Find(string name)
        {
            return FindLocal(name);
        }
    }
}
