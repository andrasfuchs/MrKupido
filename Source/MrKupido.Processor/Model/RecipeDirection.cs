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

                // ({x*})
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

                // TODO: special (culture dependent) formatting for {0T} {0N} etc. etc.
                int clauseEndIndex = 0;
                while ((clauseEndIndex = direction.IndexOf("}")) > 0)
                {
                    int clauseStartIndex = direction.LastIndexOf('{', clauseEndIndex);

                    char operandId = direction[clauseStartIndex + 1];
                    string operand = null;
                    if (Char.IsNumber(operandId))
                    {
                        object operandObj = Operands[Int32.Parse(operandId.ToString())];

                        operand = ((operandObj is IngredientGroup) ? ((IngredientGroup)operandObj).Name : operandObj.ToString());
                    }
                    else 
                    {
                        operand = NameAliasAttribute.GetDefaultName(directionType);
                    }

                    string word = "";
                    char affixId = direction[clauseEndIndex - 1];
                    if (Char.IsLetter(affixId))
                    {
                        string[] words = operand.Split(' ');
                        word = PrepareForAffix(words[words.Length - 1]);
                        operand = operand.Remove(operand.Length - word.Length);
                        VowelHarmony vh = VowelHarmonyOf(word);

                        switch (affixId)
                        {
                            case 'T':
                                vh = VowelHarmonyOf(word, true);
                                if (IsVowel(word[word.Length - 1])) word += "t";
                                else
                                {
                                    if (vh == VowelHarmony.Low) word += "ot";
                                    if (vh == VowelHarmony.HighType1) word += "et";
                                    if (vh == VowelHarmony.HighType2) word += "öt";
                                    if (vh == VowelHarmony.Mixed) word += "t";
                                }
                                break;
                            case 'R':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += "ra";
                                if (vh == VowelHarmony.HighType1) word += "re";
                                break;
                            case 'N':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += "ban";
                                if (vh == VowelHarmony.HighType1) word += "ben";
                                break;
                            case 'B':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += "ba";
                                if (vh == VowelHarmony.HighType1) word += "be";
                                break;
                            case 'V':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += (IsVowel(word[word.Length - 1]) ? "v" : "word[word.Length - 1]") + "al";
                                if (vh == VowelHarmony.HighType1) word += (IsVowel(word[word.Length - 1]) ? "v" : "word[word.Length - 1]") + "el";
                                break;
                            case 'L':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += "ról";
                                if (vh == VowelHarmony.HighType1) word += "ről";
                                break;
                            case 'K':
                                if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) word += "ból";
                                if (vh == VowelHarmony.HighType1) word += "ből";
                                break;
                            default:
                                throw new MrKupidoException("The affix id '{0}' is unknown in the '{1}' string.", affixId, direction);
                        }
                    }

                    direction = direction.Remove(clauseStartIndex, clauseEndIndex - clauseStartIndex + 1);
                    direction = direction.Insert(clauseStartIndex, operand + word);
                }

                // a(z) -> a, az
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

            //if (!String.IsNullOrWhiteSpace(Alias))
            //{
            //    sb.Append(" = ");
            //    sb.Append(Alias);
            //}
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

        private static VowelHarmony VowelHarmonyOf(string word, bool separateHighTypes = false)
        {
            int[] vowels = new int[3]; // Low, HighType1, HightType2

            foreach (char c in word)
            {
                if ((c == 'a') || (c == 'á') || (c == 'o') || (c == 'ó') || (c == 'u') || (c == 'ú')) vowels[0]++;
                if ((c == 'e') || (c == 'é') || (c == 'i') || (c == 'í')) vowels[1]++;
                if ((c == 'ö') || (c == 'ő') || (c == 'ü') || (c == 'ű')) vowels[2]++;
            }

            if (separateHighTypes == false)
            {
                vowels[1] = vowels[1] + vowels[2];
                vowels[2] = 0;
            }

            int highest = Math.Max(vowels[0], Math.Max(vowels[1], vowels[2]));
            int highestCount = vowels.Count(i => i == highest);

            if (highestCount > 1) return VowelHarmony.Mixed;
            if (vowels[0] == highest) return VowelHarmony.Low;
            if (vowels[1] == highest) return VowelHarmony.HighType1;
            if (vowels[2] == highest) return VowelHarmony.HighType2;

            return VowelHarmony.Unknown;
        }

        private static string PrepareForAffix(string word)
        {
            char[] result = word.ToCharArray();

            if (result[result.Length - 1] == 'a') result[result.Length - 1] = 'á';
            if (result[result.Length - 1] == 'e') result[result.Length - 1] = 'é';
            //if (result[result.Length - 1] == 'i') result[result.Length - 1] = 'í';
            if (result[result.Length - 1] == 'o') result[result.Length - 1] = 'ó';
            if (result[result.Length - 1] == 'ö') result[result.Length - 1] = 'ő';
            if (result[result.Length - 1] == 'u') result[result.Length - 1] = 'ú';
            if (result[result.Length - 1] == 'ü') result[result.Length - 1] = 'ű';

            return new String(result);
        }
    }
}
