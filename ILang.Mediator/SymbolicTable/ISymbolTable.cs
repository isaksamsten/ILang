using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.SymbolicTable
{
    public interface ISymbolTable
    {
        /// <summary>
        /// Get the nesting level i.e the scope of this entry
        /// </summary>
        int NestingLevel { get; }

        /// <summary>
        /// Create and get an entry in current symbolic table
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbolTableEntry Enter(string name);

        /// <summary>
        /// Find an entry
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>null if not exist</returns>
        ISymbolTableEntry Find(string name);

        /// <summary>
        /// Get the entries sorted by name
        /// </summary>
        IEnumerable<ISymbolTableEntry> SortedEntries { get; }
    }
}
