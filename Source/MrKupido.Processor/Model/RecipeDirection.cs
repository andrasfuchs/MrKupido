using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Ingredient;

namespace MrKupido.Processor.Model
{
    public class RecipeDirection : IDirection
    {
        public RecipeStage Stage { get; private set; }

        public int ActorIndex { get; private set; }
        public TimeSpan TimeToComplete { get; private set; }

        public object Command { get; private set; }
        public object[] Operands { get; private set; }
        public object Result { get; private set; }
        public string Alias { get; private set; }

        public RecipeDirection(object command, object[] operands = null, object result = null, RecipeStage stage = RecipeStage.Unknown, int actorIndex = 1)
        {
            ActorIndex = actorIndex;
            Stage = stage;

            Command = command;
            Operands = (operands == null ? new string[0] : operands);
            Result = result;

            if (result is IngredientGroup)
            {
                Alias = ((IngredientGroup)result).Name;
            }
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
