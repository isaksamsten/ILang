using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.SymbolicTable
{
    public interface ISymbolTableStack
    {
        /// <summary>
        /// Get the current nestring level
        /// </summary>
        int CurrentNestingLevel { get; }

        /// <summary>
        /// Get the local symboltable
        /// </summary>
        ISymbolTable Local { get; }

        /// <summary>
        /// Create and enter a new local table entry
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbolTableEntry EnterLocal(string name);

        /// <summary>
        /// Find a entry inte the local table
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbolTableEntry FindLocal(string name);

        /// <summary>
        /// Search the stack recursive
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbolTableEntry Find(string name);

    }
}
