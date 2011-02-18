using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILang.Mediator.SymbolicTable;

namespace ILang.Mediator
{
    public static class CrossReferencer
    {
        private static int NameWidth = 10;
        private static string NameFormat = "\n {0}\n {1}";
        private static string Space = String.Concat(Enumerable.Repeat(" ", NameWidth));
        private static string NumberLabel     = " Line numbers    ";
        private static string NumberUnderline = " ------------    ";
        private static string NumberFormat = "{0:D3}";

        private static int LabelWidth = NumberLabel.Length;

        public static void Print(ISymbolTableStack stack)
        {
            Console.WriteLine(" ======= CROSS REFERENCES  ======= \n");
            Console.Write(" Identifier" + Space + NumberLabel.PadLeft(10));
            Console.WriteLine();
            Console.Write(" " + String.Concat(Enumerable.Repeat("-", NameWidth)));
            Console.WriteLine(Space + NumberUnderline);

            PrintSymTab(stack.Local);
        }

        private static void PrintSymTab(ISymbolTable stack)
        {
            foreach (ISymbolTableEntry entry in stack.SortedEntries)
            {
                StringBuilder nums = new StringBuilder();
                foreach (int line in entry.LineNumbers)
                    nums.Append(string.Format("{0:D3} ", line));

                Console.WriteLine(" {0}" + Space + String.Concat(Enumerable.Repeat(" ", " Identifier".Length -  entry.Name.Length)) + "{1}", entry.Name, nums.ToString());
            }
        }
        
    }
}
