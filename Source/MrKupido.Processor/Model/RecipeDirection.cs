using MrKupido.Library;
using MrKupido.Library.Attributes;
using MrKupido.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

        public ITreeNode Equipment { get; private set; }

        public uint ActionDuration { get; private set; }
        public string[] ActionIconUrls { get; private set; }
        public string ActionIconUrl { get; set; }
        public bool IsPassive { get; private set; }

        public ITreeNode[] Parameters { get; private set; }

        public IDirectionSegment[] DirectionSegments { get; private set; }

        // replace exceptional words
        private string[] correctionReplacements = { "tált", "tálat", "olajt", "olajat", "olívaolajt", "olívaolajat", "mély tányér", "mélytányér", "tésztadarabokot", "tésztadarabokat", "citromhéjt", "citromhéjat", "tejt", "tejet", "sütőpapírrel", "sütőpapírral", "vízt", "vizet", "dióbélt", "dióbelet", "cukort", "cukrot", "fahéjt", "fahéjat", "sertéscombet", "sertéscombot", "sertéscombes", "sertéscombos", "csirkecombet", "csirkecombot", "csirkecombes", "csirkecombos", "tejföles", "tejfölös", "vízes", "vizes", "vajt", "vajat", "vajos", "vajas", "dióbéles", "dióbeles", "babérlevélt", "babérlevelet", "sajtes", "sajtos", "pohárt", "poharat" };

        public RecipeDirection(string languageISO, string assemblyName, string command, object[] operands = null, object result = null, RecipeStage stage = RecipeStage.Unknown, int actorIndex = 1, List<string> seenIngredients = null, IEquipment[] seenEquipment = null)
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
                this.Equipment = Cache.Equipment[eq.GetName(languageISO)];
                ActionDuration = eq.LastActionDuration;
                TimeToComplete = new TimeSpan(0, 0, (int)eq.LastActionDuration);
            }

            if (Result is IIngredientGroup)
            {
                Alias = ((IIngredientGroup)Result).GetName(languageISO);
            }


            string[] ids = Command.Split(' ');
            if (ids.Length == 2)
            {
                DirectionType = Assembly.Load(AssemblyName).GetType(ids[0]);

                Direction = NameAliasAttribute.GetName(languageISO, DirectionType, ids[1], 100);
                MemberInfo mi = DirectionType.GetMember(ids[1])[0];

                ActionIconUrls = IconUriFragmentAttribute.GetUrls(mi, "~/Content/svg/action_{0}.svg");
                IsPassive = PassiveActionAttribute.IsMethodPassiveAction(mi);
            }

            List<TreeNode> parameters = new List<TreeNode>();
            for (int i = 1; i < operands.Length; i++)
            {
                if (operands[i] is IIngredient)
                {
                    TreeNode tn = Cache.Ingredient[((IIngredient)operands[i]).GetName(languageISO)];
                    if (tn == null) tn = Cache.Recipe[((IIngredient)operands[i]).GetName(languageISO)];

                    parameters.Add(tn);
                }
                else if (operands[i] is IIngredient[])
                {
                    foreach (IIngredient ing in ((IIngredient[])operands[i]))
                    {
                        TreeNode tn = Cache.Ingredient[ing.GetName(languageISO)];
                        if (tn == null) tn = Cache.Recipe[ing.GetName(languageISO)];

                        parameters.Add(tn);
                    }
                }
                else if (operands[i] is IEquipment)
                {
                    parameters.Add(Cache.Equipment[((IEquipment)operands[i]).GetName(languageISO)]);
                }
            }
            Parameters = parameters.ToArray();

            this.DirectionSegments = GenerateSegments(languageISO, seenIngredients, seenEquipment);
        }

        private RecipeDirectionSegment[] GenerateSegments(string languageISO, List<string> seenIngredients, IEquipment[] seenEquipment)
        {
            List<RecipeDirectionSegment> result = new List<RecipeDirectionSegment>();

            // generate direction string
            if (Direction == null)
            {
                // there is no directional text defined for the method, so we need only to generate a general text (this is mainly for debugging purposes)
                result.Add(new RecipeDirectionSegment(Command));
                if (Operands.Length > 0)
                {
                    result.Add(new RecipeDirectionSegment(" "));

                    for (int i = 0; i < Operands.Length; i++)
                    {
                        result.Add(new RecipeDirectionSegmentReference(languageISO, Operands[i], seenIngredients, seenEquipment));
                        if (i < Operands.Length - 1) result.Add(new RecipeDirectionSegment(", "));
                    }
                }
            }
            else
            {
                result.AddRange(EvaluateNameAliasExpression(languageISO, Direction, Operands, Result, seenIngredients, seenEquipment));
            }

            result[0].Text = Char.ToUpper(result[0].Text[0]) + result[0].Text.Substring(1);

            return result.ToArray();
        }

        private RecipeDirectionSegment[] EvaluateNameAliasExpression(string languageISO, string expression, object[] operands, object returnedValue, List<string> seenIngredients, IEquipment[] seenEquipment)
        {
            List<RecipeDirectionSegment> result = new List<RecipeDirectionSegment>();

            int clauseStartIndex = 0;
            int clauseEndIndex = 0;
            while ((clauseStartIndex = expression.IndexOf("{", clauseStartIndex)) >= 0)
            {
                int textClauseStart = clauseEndIndex;
                int textClauseEnd = clauseStartIndex;

                clauseEndIndex = expression.IndexOf("}", clauseStartIndex);
                bool isIteration = ((clauseEndIndex >= 1) && (expression[clauseEndIndex - 1] == '*')) || ((clauseEndIndex >= 2) && (expression[clauseEndIndex - 2] == '*'));
                int iterationClauseStart = -1;
                int iterationClauseEnd = -1;


                string beforeString = "";
                string afterString = "";
                if (isIteration)
                {
                    // look for the "repeat-before" and "repeat-after" patterns
                    iterationClauseStart = expression.LastIndexOf('(', clauseStartIndex);
                    int beforeStart = iterationClauseStart + 1;
                    beforeString = expression.Substring(beforeStart, clauseStartIndex - beforeStart);

                    iterationClauseEnd = expression.IndexOf(')', clauseEndIndex);
                    int afterEnd = iterationClauseEnd - 1;
                    afterString = expression.Substring(clauseEndIndex + 1, afterEnd - clauseEndIndex);

                    textClauseEnd = beforeStart - 1;
                }

                string beginStr = expression.Substring(textClauseStart, textClauseEnd - textClauseStart);
                if (!String.IsNullOrEmpty(beginStr))
                {
                    result.Add(new RecipeDirectionSegment(beginStr));
                }

                // { ... }   there is a clause between the brackets
                string clause = expression.Substring(clauseStartIndex + 1, clauseEndIndex - clauseStartIndex - 1);

                string propertyAccessorStr = null;

                if (clause.IndexOf(".") >= 0)
                {
                    propertyAccessorStr = clause.Substring(clause.IndexOf("."), clause.LastIndexOf(".") - clause.IndexOf(".") + 1);
                    clause = clause.Replace(propertyAccessorStr, "");
                }

                // affix is always the last character
                char affixId = clause.Length == 0 ? '_' : clause[clause.Length - 1];
                if (!Char.IsLetter(affixId)) affixId = Char.MinValue;

                // operand can be a number OR a number followed by an asterix (in case of an iteration)
                int removeChartsFromTheEnd = 0;
                if (isIteration) removeChartsFromTheEnd++;
                if (affixId != Char.MinValue) removeChartsFromTheEnd++;

                string operandIndexStr = clause.Substring(0, clause.Length - removeChartsFromTheEnd);
                int operandIndex = String.IsNullOrEmpty(operandIndexStr) || (operandIndexStr == "-") ? 0 : Int32.Parse(operandIndexStr) + 1;

                object operand = operandIndexStr == "-" ? returnedValue : operands[operandIndex];

                if (propertyAccessorStr != null)
                {
                    string[] propertyNames = propertyAccessorStr.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < propertyNames.Length; i++)
                    {
                        PropertyInfo property = operand.GetType().GetProperty(propertyNames[i]);

                        // if this property is the last to evaluate
                        if (i == propertyNames.Length - 1)
                        {
                            // let's try to get its NameAlias instead of its value
                            string propertyNameAlias = NameAliasAttribute.GetName(languageISO, operand.GetType(), propertyNames[i]);
                            if (String.IsNullOrEmpty(propertyNameAlias))
                            {
                                operand = property.GetValue(operand, null);
                            }
                            else
                            {
                                result.AddRange(EvaluateNameAliasExpression(languageISO, propertyNameAlias, new object[] { operand }, null, seenIngredients, seenEquipment));
                                operand = result.Last().ToString();
                                result.RemoveAt(result.Count - 1);
                            }
                        }
                        else
                        {
                            operand = property.GetValue(operand, null);
                        }

                        if (operand == null) break;
                    }
                }

                Array items = new object[0];
                if (isIteration)
                {
                    if (!(operand is Array)) throw new MrKupidoException("If you use the {0*} clause in the action, you must pass an array as a parameter. The parameter number " + (operandIndex - 1) + " is not an array.");

                    items = (Array)operand;
                }
                else
                {
                    items = new object[] { operand };
                }


                if (items.Length > 0)
                {
                    foreach (object item in items)
                    {
                        if (!String.IsNullOrEmpty(beforeString)) result.Add(new RecipeDirectionSegment(beforeString));

                        //result.Add(new RecipeDirectionSegmentReference(languageISO, item, seenIngredients));

                        RecipeDirectionSegmentReference rdsr = new RecipeDirectionSegmentReference(languageISO, item, seenIngredients, seenEquipment);
                        if (!String.IsNullOrEmpty(rdsr.Name))
                        {
                            rdsr.Name = Affixate(PrepareForAffix(rdsr.Name), 'S');
                        }

                        // TODO: special (culture dependent) formatting for {0T} {0N} etc. etc.
                        if (affixId != Char.MinValue)
                        {
                            // only the last word needs affixation
                            string[] words = rdsr.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            string lastWord = PrepareForAffix(words[words.Length - 1]);
                            lastWord = Affixate(lastWord, affixId);

                            if (!String.IsNullOrEmpty(rdsr.IconAlt) && (lastWord.StartsWith(rdsr.IconAlt)))
                            {
                                lastWord = lastWord.Replace(rdsr.IconAlt, "-");
                            }

                            // replace the last word
                            rdsr.Text = rdsr.Text.Remove(rdsr.Text.Length - words[words.Length - 1].Length) + lastWord;
                        }

                        result.Add(rdsr);

                        if (!String.IsNullOrEmpty(afterString)) result.Add(new RecipeDirectionSegment(afterString));
                    }
                    if (!String.IsNullOrEmpty(afterString)) result.RemoveAt(result.Count - 1);
                }

                clauseEndIndex = iterationClauseEnd >= 0 ? iterationClauseEnd + 1 : clauseEndIndex + 1;
                clauseStartIndex = clauseEndIndex;
            }

            string endStr = expression.Substring(clauseEndIndex);
            if (!String.IsNullOrEmpty(endStr))
            {
                result.Add(new RecipeDirectionSegment(endStr));
            }

            // a(z) -> a, az
            int azIndex = 0;
            int segmentIndex = 0;

            while (segmentIndex < result.Count)
            {
                while ((azIndex = result[segmentIndex].TextOnly().IndexOf("a(z)")) > 0)
                {
                    bool makeAz = false;

                    int charToCheckIndex = azIndex + 5;
                    int charToCheckSegmentIndex = segmentIndex;
                    if (charToCheckIndex >= result[segmentIndex].TextOnly().Length)
                    {
                        charToCheckSegmentIndex++;
                        charToCheckIndex -= result[segmentIndex].TextOnly().Length;
                    }
                    char charToCheck = result[charToCheckSegmentIndex].TextOnly()[charToCheckIndex];

                    if (StringUtils.IsVowel(charToCheck)) makeAz = true;
                    //if ((charToCheck == '5') || (charToCheck == '1')) makeAz = true;
                    //if ((charToCheck == '1') && Char.IsNumber(result[charToCheckSegmentIndex].TextOnly()[charToCheckIndex + 1])) makeAz = false;

                    if (makeAz)
                    {
                        result[segmentIndex].Text = result[segmentIndex].Text.Remove(azIndex + 3, 1).Remove(azIndex + 1, 1);
                    }
                    else
                    {
                        result[segmentIndex].Text = result[segmentIndex].Text.Remove(azIndex + 1, 3);
                    }
                }

                segmentIndex++;
            }


            // replace the word which are irregular
            foreach (RecipeDirectionSegment segment in result)
            {
                segment.Text = ReplaceIrregularWord(segment.Text);

                if (segment is RecipeDirectionSegmentReference)
                {
                    ((RecipeDirectionSegmentReference)segment).Name = ReplaceIrregularWord(((RecipeDirectionSegmentReference)segment).Name);
                }
            }

            return result.ToArray();
        }

        private string ReplaceIrregularWord(string word)
        {
            string result = word;

            if (result == "-") result = "";

            result = " " + result + " ";

            for (int i = 0; i < correctionReplacements.Length / 2; i++)
            {
                result = result.Replace(" " + correctionReplacements[i * 2] + " ", " " + correctionReplacements[i * 2 + 1] + " ");
            }

            result = result.Substring(1, result.Length - 2);

            return result;
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

            if (highestCount > 1) return VowelHarmony.Mixed; // in this case we should check out the type of the last one of them
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
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += (StringUtils.IsVowel(result[result.Length - 1]) ? 'v' : result[result.Length - 1]) + "al";
                    if (vh == VowelHarmony.HighType1) result += (StringUtils.IsVowel(result[result.Length - 1]) ? 'v' : result[result.Length - 1]) + "el";
                    break;
                case 'L':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ról";
                    if (vh == VowelHarmony.HighType1) result += "ről";
                    break;
                case 'K':
                    if ((vh == VowelHarmony.Low) || (vh == VowelHarmony.Mixed)) result += "ból";
                    if (vh == VowelHarmony.HighType1) result += "ből";
                    break;
                case 'H':
                    if (StringUtils.IsVowel(result[result.Length - 1]))
                    {
                        result += "n";
                    }
                    else
                    {
                        if (vh == VowelHarmony.Low) result += "on";
                        if (vh == VowelHarmony.HighType1) result += "en";
                        if (vh == VowelHarmony.HighType2) result += "ön";
                    }
                    break;
                case 'S':
                    if (StringUtils.IsVowel(result[result.Length - 1]))
                    {
                        result += "s";
                    }
                    else
                    {
                        if (vh == VowelHarmony.Low) result += "os";
                        if (vh == VowelHarmony.HighType1) result += "es";
                        if (vh == VowelHarmony.HighType2) result += "ös";
                        if (vh == VowelHarmony.Mixed) result += "s";
                    }
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
