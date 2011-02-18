using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ILang.Mediator.Intermediate;

namespace ILang.Mediator
{
    public class SyntaxTreePrinter
    {
        private TextWriter sw;
        private string _indent = "  ";
        private string _intendation;

        public SyntaxTreePrinter(TextWriter output)
        {
            sw = output;
        }

        public void Print(ICode icode)
        {
            sw.WriteLine(" ======= Intermediate Code ======= ");
            PrintNode(new ICodeNode[] { icode.Root as IntermediateCodeNode }, 0);
            sw.WriteLine();
        }

        private void PrintNode(IEnumerable<ICodeNode> node, int depth)
        {
            if (node != null)
            {
                foreach (var n in node)
                {
                    sw.Write(string.Concat(Enumerable.Repeat(_indent, depth)) + " [" + n.ToString() + "] attr( ");
                    foreach (var kv in n as IntermediateCodeNode)
                    {
                        sw.Write(kv.Key + "=" + kv.Value + " ");
                    }
                    sw.WriteLine(") ");
                    PrintNode(n.Children, depth + 1);
                }
            }
        }
    }
}
