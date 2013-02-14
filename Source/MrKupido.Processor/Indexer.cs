using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using MrKupido.Processor.Model;
using MrKupido.Library.Attributes;
using System.Diagnostics;

namespace MrKupido.Processor
{
    public class Indexer
    {
        private string[] allowedLanguageISOCodes = { "hun", "eng" };
        private static IComparer<String> accentFolderStringComparer = new AccentFolderStringComparer();

        private SortedList<string, TreeNode> classNameIndex = new SortedList<string, TreeNode>();
        private Dictionary<string, SortedList<string, TreeNode>> nameIndex = new Dictionary<string, SortedList<string, TreeNode>>();
        private Dictionary<string, SortedList<string, TreeNode>> uniqueNameIndex = new Dictionary<string, SortedList<string, TreeNode>>();

        private List<TreeNode> all = new List<TreeNode>();
        public string LanguageISO { get; private set; }

        public TreeNode[] All 
        { 
            get
            {
                return all.ToArray();
            }
        }

        public Indexer(TreeNode root, string languageISO)
        {
            this.LanguageISO = languageISO;

            all.Clear();
            AddToIndex(root);
        }

        private void AddToIndex(TreeNode node)
        {
            if (node.LanguageISO != this.LanguageISO) return;

            all.Add(node);
            
            if (classNameIndex.ContainsKey(node.ClassName)) throw new MrKupidoException("Class-name index already has an item with the key '{0}'", node.ClassName);
            classNameIndex.Add(node.ClassFullName, node);

            foreach (string languageISO in allowedLanguageISOCodes)
            {
                if ((!nameIndex.ContainsKey(languageISO)) || (nameIndex[languageISO] == null)) nameIndex[languageISO] = new SortedList<string, TreeNode>();
                if ((!uniqueNameIndex.ContainsKey(languageISO)) || (uniqueNameIndex[languageISO] == null)) uniqueNameIndex[languageISO] = new SortedList<string, TreeNode>();

                foreach (NameAliasAttribute name in NameAliasAttribute.GetNames(node.ClassType, null, languageISO))
                {
                    if (nameIndex.ContainsKey(name.Name)) throw new MrKupidoException("Name index already has an item with the key '{0}'", name.Name);
                    nameIndex[languageISO].Add(name.Name, node);

                    string uniqueName = name.Name.ToUniqueString();
                    if (uniqueNameIndex.ContainsKey(uniqueName)) throw new MrKupidoException("Unique-name index already has an item with the key '{0}'", uniqueName);
                    uniqueNameIndex[languageISO].Add(uniqueName, node);
                }
            }

            foreach (TreeNode tn in node.Children)
            {
                AddToIndex(tn);
            }
        }
        
        public TreeNode GetByClassName(string className)
        {
            TreeNode tn = null;
            classNameIndex.TryGetValue(className, out tn);

            if (tn == null) Trace.TraceError("The class '{0}' was not found in the tree.", className);

            return tn;
        }

        public TreeNode GetByUniqueName(string uniqueName, string languageISO)
        {
            TreeNode tn = null;
            uniqueNameIndex[languageISO].TryGetValue(uniqueName, out tn);

            if (tn == null) Trace.TraceError("The class with unique-name '{0}' was not found in the tree.", uniqueName);

            return tn;
        }

        public TreeNode GetByName(string name, string languageISO)
        {
            TreeNode tn = null;
            nameIndex[languageISO].TryGetValue(name, out tn);

            if (tn == null) Trace.TraceError("The class with name '{0}' was not found in the tree.", name);

            return tn;
        }

        public Dictionary<string, TreeNode> QueryByName(string query, string languageISO)
        {
            Dictionary<string, TreeNode> result = new Dictionary<string, TreeNode>();
            int index = Array.BinarySearch<string>(nameIndex[languageISO].Keys.ToArray(), query, accentFolderStringComparer);

            if (index < 0) index = -index - 1;

            while ((index < nameIndex[languageISO].Keys.Count) && (accentFolderStringComparer.Compare(nameIndex[languageISO].Keys[index].Substring(0, Math.Min(nameIndex[languageISO].Keys[index].Length, query.Length)), query) == 0))
            {
                result.Add(nameIndex[languageISO].Keys[index], nameIndex[languageISO][nameIndex[languageISO].Keys[index]]);
                index++;
            }

            return result;
        }
    
    }
}
