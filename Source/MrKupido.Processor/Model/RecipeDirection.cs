using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;

namespace MrKupido.Processor.Model
{
    public class RecipeDirection : IDirection
    {
        public string Command { get; private set; }
        public string[] Operands { get; private set; }
        public string Alias { get; private set; }

        public RecipeDirection(string command, string[] operands = null, string alias = null)
        {
            Command = command;
            Operands = (operands == null ? new string[0] : operands);
            Alias = alias;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Command);
            sb.Append(" a következőket: ");

            foreach (string operand in Operands)
            {
                sb.Append(operand);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 3, 2);

            if (!String.IsNullOrWhiteSpace(Alias))
            {
                sb.Append(" = ");
                sb.Append(Alias);
            }
            Char.ToUpperInvariant(sb[0]);

            return sb.ToString();
        }
    }
}
