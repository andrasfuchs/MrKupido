using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using MrKupido.Processor.Model;
using MrKupido.Library.Attributes;

namespace MrKupido.Processor
{
    public class Indexer
    {
        private string[] allowedLanguageISOCodes = { "hun", "eng" };

        private Dictionary<string, TreeNode> classNameIndex = new Dictionary<string, TreeNode>();
        private Dictionary<string, Dictionary<string, TreeNode>> nameIndex = new Dictionary<string, Dictionary<string, TreeNode>>();
        private Dictionary<string, Dictionary<string, TreeNode>> uniqueNameIndex = new Dictionary<string, Dictionary<string, TreeNode>>();

        public Indexer(TreeNode root)
        {
            classNameIndex.Clear();

            AddToIndex(root);
        }

        private void AddToIndex(TreeNode node)
        {
            classNameIndex.Add(node.ClassName, node);

            foreach (string languageISO in allowedLanguageISOCodes)
            {
                if ((!nameIndex.ContainsKey(languageISO)) || (nameIndex[languageISO] == null)) nameIndex[languageISO] = new Dictionary<string, TreeNode>();
                if ((!uniqueNameIndex.ContainsKey(languageISO)) || (uniqueNameIndex[languageISO] == null)) uniqueNameIndex[languageISO] = new Dictionary<string, TreeNode>();

                foreach (NameAliasAttribute name in NameAliasAttribute.GetNameAliases(node.ClassType, languageISO))
                {
                    nameIndex[languageISO].Add(name.Name, node);
                    uniqueNameIndex[languageISO].Add(name.Name.ToUniqueString(), node);
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

            if (tn == null) throw new MrKupidoException("The class '{0}' was not found in the tree.", className);

            return tn;
        }

        public TreeNode GetByUniqueName(string uniqueName, string languageISO)
        {
            TreeNode tn = null;
            uniqueNameIndex[languageISO].TryGetValue(uniqueName, out tn);

            if (tn == null) throw new MrKupidoException("The class with unique-name '{0}' was not found in the tree.", uniqueName);

            return tn;
        }

        public TreeNode GetByName(string name, string languageISO)
        {
            TreeNode tn = null;
            uniqueNameIndex[languageISO].TryGetValue(name, out tn);

            if (tn == null) throw new MrKupidoException("The class with name '{0}' was not found in the tree.", name);

            return tn;
        }
    }
}
