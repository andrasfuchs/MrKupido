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
        private string Direction { get; set; }
        private Type DirectionType { get; set; }
        public object[] Operands { get; private set; }
        public object Result { get; private set; }
        public string Alias { get; private set; }
        public uint ActionDuration { get; private set; }
        public string[] IconUrls { get; private set; }
        public string IconUrl { get; set; }
        public bool IsPassive { get; private set; }

        public RecipeDirection(string assemblyName, string command, object[] operands = null, object result = null, RecipeStage stage = RecipeStage.Unknown, int actorIndex = 1)
        {
            ActorIndex = actorIndex;
            Stage = stage;

            AssemblyName = assemblyName;
            Command = command;
            Operands = (operands == null ? new string[0] : operands);
            Result = result;

            if (operands[0] is IEquipment)
            {
                IEquipment eq = ((IEquipment)operands[0]);
                ActionDuration = eq.LastActionDuration;
                TimeToComplete = new TimeSpan(0, 0, (int)eq.LastActionDuration);
            }

            if (result is IngredientGroup)
            {
                Alias = ((IngredientGroup)result).Name;
            }


            string[] ids = Command.Split(' ');
            if (ids.Length == 2)
            {
                DirectionType = Assembly.Load(AssemblyName).GetType(ids[0]);
                Direction = NameAliasAttribute.GetMethodName(DirectionType, ids[1]);
                MemberInfo mi = DirectionType.GetMember(ids[1])[0];

                IconUrls = IconUriFragmentAttribute.GetUrls(mi, "~/Content/svg/action_{0}.svg");
                IsPassive = PassiveActionAttribute.IsMethodPassiveAction(mi);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            object[] operands = new object[Operands.Length - 1];
            Array.Copy(Operands, 1, operands, 0, operands.Length);

            if (Direction == null)
            {
                sb.Append(Command);

                sb.Append(' ');
                if (operands.Length > 0)
                {
                    foreach (object operand in operands)
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
                string[] ops = new string[operands.Length];
                for (int i = 0; i < ops.Length; i++)
                {
                    if (operands[i] == null)
                    {
                        ops[i] = "(null)";
                    }
                    else if (operands[i] is IngredientGroup)
                    {
                        ops[i] = ((IngredientGroup)operands[i]).Name;
                    }
                    else
                    {
                        ops[i] = operands[i].ToString();
                    }
                }

                // ({x*})
                int starIndex = 0;
                while ((starIndex = Direction.IndexOf("*}")) > 0)
                {
                    int operandIndex = Int32.Parse(Direction[starIndex - 1].ToString());

                    int beforeStart = Direction.LastIndexOf('(', starIndex)+1;
                    string beforeString = Direction.Substring(beforeStart, starIndex - 2 - beforeStart);

                    int afterEnd = Direction.IndexOf(')', starIndex + 2) - 1;
                    string afterString = Direction.Substring(starIndex + 2, afterEnd - starIndex - 1);

                    Direction = Direction.Remove(beforeStart-1, afterEnd - beforeStart + 3);

                    StringBuilder dirSB = new StringBuilder();
                    object[] items = (object[])operands[operandIndex];
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

                    Direction = Direction.Insert(beforeStart - 1, dirSB.ToString());
                }

                // TODO: special (culture dependent) formatting for {0T} {0N} etc. etc.
                int clauseEndIndex = 0;
                while ((clauseEndIndex = Direction.IndexOf("}")) > 0)
                {
                    int clauseStartIndex = Direction.LastIndexOf('{', clauseEndIndex);

                    char operandId = Direction[clauseStartIndex + 1];
                    string operand = null;
                    if (Char.IsNumber(operandId))
                    {
                        object operandObj = operands[Int32.Parse(operandId.ToString())];

                        if (operandObj != null)
                        {
                            operand = ((operandObj is IngredientGroup) ? ((IngredientGroup)operandObj).Name : operandObj.ToString());
                        }
                    }
                    else 
                    {
                        operand = NameAliasAttribute.GetDefaultName(Operands[0].GetType());
                    }

                    string word = "";
                    if (operand != null)
                    {
                        char affixId = Direction[clauseEndIndex - 1];
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
                                    throw new MrKupidoException("The affix id '{0}' is unknown in the '{1}' string.", affixId, Direction);
                            }
                        }
                    }
                    else
                    {
                        operand = "(null)";
                    }

                    Direction = Direction.Remove(clauseStartIndex, clauseEndIndex - clauseStartIndex + 1);
                    Direction = Direction.Insert(clauseStartIndex, operand + word);
                }

                // a(z) -> a, az
                int azIndex = 0;
                while ((azIndex = Direction.IndexOf("a(z)")) > 0)
                {
                    bool makeAz = false;
                    if (IsVowel(Direction[azIndex + 5])) makeAz = true;
                    if ((Direction[azIndex + 5] == '5') || (Direction[azIndex + 5] == '1')) makeAz = true;

                    if (makeAz)
                    {
                        Direction = Direction.Remove(azIndex + 3, 1).Remove(azIndex + 1, 1);
                    }
                    else
                    {
                        Direction = Direction.Remove(azIndex + 1, 3);
                    }
                }

                sb.Append(Direction);
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
