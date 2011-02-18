using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.SymbolicTable;

namespace ILang.Mediator
{
    public static class SymbolicTableFactory
    {
        public static ISymbolTableStack CreateStack()
        {
            return new ILangSymbolTableStack();
        }

        public static ISymbolTable CreateTable(int level)
        {
            return new ILangSymbolTable(level);
        }

        public static ISymbolTableEntry CreateEntry(string name, ISymbolTable table)
        {
            return new ILangSymbolTableEntry(name, table);
        }
    }
}
