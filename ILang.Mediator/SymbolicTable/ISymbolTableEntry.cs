using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILang.Mediator.SymbolicTable
{
    public interface ISymbolTableEntry
    {
        /// <summary>
        /// Get the name of the entry
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get the symbolic table (parent)
        /// </summary>
        ISymbolTable SymbolicTable { get; }

        /// <summary>
        /// Append a line number where it is seen
        /// </summary>
        /// <param name="line"></param>
        void AppendLineNumber(int line);

        /// <summary>
        /// Get the line number
        /// </summary>
        IEnumerable<int> LineNumbers { get; }

        /// <summary>
        /// Set an attribute to value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetAttribute(ISymbolTableKey key, object value);

        /// <summary>
        /// Get an attribute
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetAttribute(ISymbolTableKey key);
    }
}
