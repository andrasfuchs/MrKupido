using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;
using System.Reflection;
using MrKupido.Library.Equipment;
using System.Collections;
using MrKupido.Utils;

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
        public IDirectionSegment[] DirectionSegments { get; private set; }

        public RecipeDirection(ref int idCounter, string assemblyName, string command, object[] operands = null, object result = null, RecipeStage stage = RecipeStage.Unknown, int actorIndex = 1)
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

                if (eq is Container)
                {
                    if (((Container)eq).Id == 0)
                    {
                        ((Container)eq).Id = ++idCounter;
                    }
                }
            }

            if (result is IngredientGroup)
            {
                Alias = ((IngredientGroup)result).Name;
                if (((IngredientGroup)result).Id == 0)
                {
                    ((IngredientGroup)result).Id = ++idCounter;
                }
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

            this.DirectionSegments = GenerateSegments();
        }

        private RecipeDirectionSegment[] GenerateSegments()
        {
            List<RecipeDirectionSegment> result = new List<RecipeDirectionSegment>();

            // generate direction string
            if (Direction == null)
            {
                // there is no directional text defined for the method, so we need only generate a general text (this is mainly for debugging purposes)
                result.Add(new RecipeDirectionSegment(Command));
                if (Operands.Length > 0)
                {
                    result.Add(new RecipeDirectionSegment(" "));

                    for (int i = 0; i < Operands.Length; i++)
                    {
                        result.Add(new RecipeDirectionSegmentReference(Operands[i]));
                        if (i < Operands.Length - 1) result.Add(new RecipeDirectionSegment(", "));
                    }
                }
            }
            else
            {
                int clauseStartIndex = 0;
                int clauseEndIndex = 0;
                while ((clauseStartIndex = Direction.IndexOf("{", clauseStartIndex)) > 0)
                {
                    int textClauseStart = clauseEndIndex;
                    int textClauseEnd = clauseStartIndex;

                    clauseEndIndex = Direction.IndexOf("}", clauseStartIndex);
                    bool isIteration = Direction[clauseEndIndex - 1] == '*';
                    int iterationClauseStart = -1;
                    int iterationClauseEnd = -1;


                    string beforeString = "";
                    string afterString = "";
                    if (isIteration)
                    {
                        // look for the "repeat-before" and "repeat-after" patterns
                        iterationClauseStart = Direction.LastIndexOf('(', clauseStartIndex);
                        int beforeStart = iterationClauseStart + 1;
                        beforeString = Direction.Substring(beforeStart, clauseStartIndex - beforeStart);

                        iterationClauseEnd = Direction.IndexOf(')', clauseEndIndex);
                        int afterEnd = iterationClauseEnd - 1;
                        afterString = Direction.Substring(clauseEndIndex + 1, afterEnd - clauseEndIndex);

                        textClauseEnd = beforeStart - 1;
                    }

                    result.Add(new RecipeDirectionSegment(Direction.Substring(textClauseStart, textClauseEnd - textClauseStart)));
                    if (clauseEndIndex - clauseStartIndex == 1)
                    {
                        // {}
                        result.Add(new RecipeDirectionSegmentReference(Operands[0]));
                    }
                    else 
                    {
                        char affixId = Direction[clauseEndIndex - 1];
                        if (!Char.IsLetter(affixId)) affixId = Char.MinValue;

                        string operandIndexStr = Direction.Substring(clauseStartIndex + 1, clauseEndIndex - clauseStartIndex - 1 - (isIteration || affixId != Char.MinValue ? 1 : 0));
                        int operandIndex = String.IsNullOrEmpty(operandIndexStr) ? 0 : Int32.Parse(operandIndexStr) + 1;
                        object operand = Operands[operandIndex];

                        if (isIteration)
                        {
                            Array items = (Array)operand;

                            if (items.Length > 0)
                            {
                                foreach (object item in items)
                                {
                                    if (!String.IsNullOrEmpty(beforeString)) result.Add(new RecipeDirectionSegment(beforeString));
                                    result.Add(new RecipeDirectionSegmentReference(item));
                                    if (!String.IsNullOrEmpty(afterString)) result.Add(new RecipeDirectionSegment(afterString));
                                }
                                if (!String.IsNullOrEmpty(afterString)) result.RemoveAt(result.Count - 1);
                            }
                        }
                        else
                        {
                            RecipeDirectionSegmentReference rdsr = new RecipeDirectionSegmentReference(operand);

                            // TODO: special (culture dependent) formatting for {0T} {0N} etc. etc.
                            if (affixId != Char.MinValue)
                            {
                                // only the last word needs affixation
                                string[] words = rdsr.Text.Split(' ');
                                string lastWord = PrepareForAffix(words[words.Length - 1]);
                                lastWord = Affixate(lastWord, affixId);

                                if (lastWord.StartsWith("--"))
                                {
                                    lastWord = lastWord.Replace("--", "-");
                                }

                                // replace the last word
                                rdsr.Text = rdsr.Text.Remove(rdsr.Text.Length - words[words.Length - 1].Length) + lastWord;
                            }

                            result.Add(rdsr);
                        }
                    }

                    clauseEndIndex = iterationClauseEnd >= 0 ? iterationClauseEnd + 1 : clauseEndIndex + 1;
                    clauseStartIndex = clauseEndIndex;
                }
                result.Add(new RecipeDirectionSegment(Direction.Substring(clauseEndIndex)));
            }

            if ((Result != null) && (Result is IngredientGroup) && (!String.IsNullOrEmpty(((IngredientGroup)Result).IconUrl)) && (Result != Operands[Operands.Length - 1]))
            {
                IngredientGroup ig = ((IngredientGroup)Result);

                result.Add(new RecipeDirectionSegment(" => "));
                result.Add(new RecipeDirectionSegmentReference(ig));
            }


            // a(z) -> a, az
            //int azIndex = 0;
            //while ((azIndex = text.IndexOf("a(z)")) > 0)
            //{
            //    bool makeAz = false;
            //    if (StringUtils.IsVowel(text[azIndex + 5])) makeAz = true;
            //    if ((text[azIndex + 5] == '5') || (text[azIndex + 5] == '1')) makeAz = true;
            //    if ((text[azIndex + 5] == '1') && Char.IsNumber(text[azIndex + 6])) makeAz = false;

            //    if (makeAz)
            //    {
            //        text = text.Remove(azIndex + 3, 1).Remove(azIndex + 1, 1);
            //    }
            //    else
            //    {
            //        text = text.Remove(azIndex + 1, 3);
            //    }
            //}


            return result.ToArray();
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

        private static string Affixate(string word, char affixId)
        {
            string result = word;

            VowelHarmony vh = VowelHarmonyOf(result);
            switch (affixId)
            {
                case 'T':
                    vh = VowelHarmonyOf(result, true);
                    if ((StringUtils.IsVowel(result[result.Length - 1]))
                        || ((result.Length >= 2) && StringUtils.IsVowel(result[result.Length - 2]) &&
                            ((result[result.Length - 1] == 'j') || (result[result.Length - 1] == 'l') || (result[result.Length - 1] == 'n') || (result[result.Length - 1] == 'r')
                            || (result[result.Length - 1] == 'n') || (result[result.Length - 1] == 'r') || (result[result.Length - 1] == 's') || (result[result.Length - 1] == 'z')))
                        || ((result.Length >= 3) && StringUtils.IsVowel(result[result.Length - 3]) &&
                        (((result[result.Length - 2] == 'l') && (result[result.Length - 1] == 'y'))
                        || ((result[result.Length - 2] == 'n') && (result[result.Length - 1] == 'y'))
                        || ((result[result.Length - 2] == 's') && (result[result.Length - 1] == 'z'))
                        || ((result[result.Length - 2] == 'z') && (result[result.Length - 1] == 's'))))
                        ) result += "t";
                    else
                    {
                        if (vh == VowelHarmony.Low) result += "ot";
                        if (vh == VowelHarmony.HighType1) result += "et";
                        if (vh == VowelHarmony.HighType2) result += "öt";
                        if (vh == VowelHarmony.Mixed) result += "t";
                    }
                    break;
                case 'R':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ra";
                    if (vh == VowelHarmony.HighType1) result += "re";
                    break;
                case 'N':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ban";
                    if (vh == VowelHarmony.HighType1) result += "ben";
                    break;
                case 'B':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ba";
                    if (vh == VowelHarmony.HighType1) result += "be";
                    break;
                case 'V':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += (StringUtils.IsVowel(result[result.Length - 1]) ? "v" : "result[result.Length - 1]") + "al";
                    if (vh == VowelHarmony.HighType1) result += (StringUtils.IsVowel(result[result.Length - 1]) ? "v" : "result[result.Length - 1]") + "el";
                    break;
                case 'L':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ról";
                    if (vh == VowelHarmony.HighType1) result += "ről";
                    break;
                case 'K':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ból";
                    if (vh == VowelHarmony.HighType1) result += "ből";
                    break;
                default:
                    throw new MrKupidoException("The affix id '{0}' is unknown, so the word '{1}' can't be processed.", affixId, word);
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (RecipeDirectionSegment segment in DirectionSegments)
            {
                sb.Append(segment.Text);
            }

            sb[0] = Char.ToUpperInvariant(sb[0]);
            
            return sb.ToString();
        }
    }
}
