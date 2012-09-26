using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;
using System.Reflection;

namespace MrKupido.Processor.Model
{
    public class RecipeDirection : IDirection
    {
        public RecipeStage Stage { get; private set; }

        public int ActorIndex { get; private set; }
        public TimeSpan TimeToComplete { get; private set; }

        public string AssemblyName { get; private set; }
        public string Command { get; private set; }
        public object[] Operands { get; private set; }
        public object Result { get; private set; }
        public string Alias { get; private set; }
        public uint ActionDuration { get; private set; }

        public RecipeDirection(string assemblyName, string command, object[] operands = null, object result = null, RecipeStage stage = RecipeStage.Unknown, int actorIndex = 1, uint actionDuration = 0)
        {
            ActorIndex = actorIndex;
            Stage = stage;

            AssemblyName = assemblyName;
            Command = command;
            Operands = (operands == null ? new string[0] : operands);
            Result = result;
            ActionDuration = actionDuration;
            TimeToComplete = new TimeSpan(0, 0, (int)actionDuration);

            if (result is IngredientGroup)
            {
                Alias = ((IngredientGroup)result).Name;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string[] ids = Command.Split(' ');
            string direction = null;
            Type directionType = null;

            if (ids.Length == 2)
            {
                directionType = Assembly.Load(AssemblyName).GetType(ids[0]);
                direction = NameAliasAttribute.GetMethodName(directionType, ids[1]);
            }

            if (direction == null)
            {
                sb.Append(Command);

                sb.Append(' ');
                if (Operands.Length > 0)
                {
                    foreach (object operand in Operands)
                    {
                        sb.Append(operand);
                        sb.Append(", ");
                    }
                    sb.Remove(sb.Length - 3, 2);
                }
                sb.Append(' ');
            }
            else
            {
                // TODO: special (culture dependent) formatting for {0T} {0N} etc. etc.
                direction = direction.Replace("T}", "}et");
                direction = direction.Replace("R}", "}re");
                direction = direction.Replace("N}", "}ben");
                direction = direction.Replace("B}", "}be");
                direction = direction.Replace("V}", "}val");
                direction = direction.Replace("L}", "}ról");
                direction = direction.Replace("K}", "}ből");

                if (directionType != null)
                {
                    direction = direction.Replace("{}", NameAliasAttribute.GetDefaultName(directionType));
                }

                string[] ops = new string[Operands.Length];
                for (int i = 0; i < ops.Length; i++)
                {
                    if (Operands[i] is IngredientGroup)
                    {
                        ops[i] = ((IngredientGroup)Operands[i]).Name;
                    }
                    else
                    {
                        ops[i] = Operands[i].ToString();
                    }
                }

                int starIndex = 0;
                while ((starIndex = direction.IndexOf("*}")) > 0)
                {
                    int operandIndex = Int32.Parse(direction[starIndex - 1].ToString());

                    int beforeStart = direction.LastIndexOf('(', starIndex)+1;
                    string beforeString = direction.Substring(beforeStart, starIndex - 2 - beforeStart);

                    int afterEnd = direction.IndexOf(')', starIndex + 2) - 1;
                    string afterString = direction.Substring(starIndex + 2, afterEnd - starIndex - 1);

                    direction = direction.Remove(beforeStart-1, afterEnd - beforeStart + 3);

                    StringBuilder dirSB = new StringBuilder();
                    object[] items = (object[])Operands[operandIndex];
                    if (items.Length > 0)
                    {
                        foreach (object item in items)
                        {
                            dirSB.Append(beforeString);
                            dirSB.Append(item);
                            dirSB.Append(afterString);
                        }
                        dirSB.Remove(dirSB.Length - afterString.Length, afterString.Length);
                    }

                    direction = direction.Insert(beforeStart - 1, dirSB.ToString());
                }

                direction = String.Format(direction, ops);

                int azIndex = 0;
                while ((azIndex = direction.IndexOf("a(z)")) > 0)
                {
                    bool makeAz = false;
                    if (IsVowel(direction[azIndex + 5])) makeAz = true;
                    if ((direction[azIndex + 5] == '5') || (direction[azIndex + 5] == '1')) makeAz = true;

                    if (makeAz)
                    {
                        direction = direction.Remove(azIndex + 3, 1).Remove(azIndex + 1, 1);
                    }
                    else
                    {
                        direction = direction.Remove(azIndex + 1, 3);
                    }
                }

                sb.Append(direction);
            }

            if (!String.IsNullOrWhiteSpace(Alias))
            {
                sb.Append(" = ");
                sb.Append(Alias);
            }
            sb[0] = Char.ToUpperInvariant(sb[0]);

            return sb.ToString();
        }

        private static bool IsVowel(char letter)
        {
            return
                letter == 'a' ||
                letter == 'á' ||
                letter == 'e' ||
                letter == 'é' ||
                letter == 'i' ||
                letter == 'í' ||
                letter == 'o' ||
                letter == 'ó' ||
                letter == 'ö' ||
                letter == 'ő' ||
                letter == 'u' ||
                letter == 'ú' ||
                letter == 'ü' ||
                letter == 'ű';
        }
    }
}
