using MrKupido.Library;
using MrKupido.Processor.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MrKupido.Processor
{
    public class BaseCache
    {
        protected string language;

        public Indexer Indexer { get; protected set; }

        public bool WasInitialized = false;

        public TreeNode this[string name]
        {
            get
            {
                if (String.IsNullOrEmpty(language)) throw new MrKupidoException("You must set the language of the cache before requesting data.");

                TreeNode result = null;

                if (result == null)
                {
                    result = Indexer.GetByUniqueName(name, language);
                }

                if (result == null)
                {
                    result = Indexer.GetByClassName(name);
                }

                if (result == null)
                {
                    result = Indexer.GetByName(name, language);
                }

                if (result == null)
                {
                    Trace.TraceError("The class with unique-name/class name/name '{0}' was not found in the tree.", name);
                }

                return result;
            }
        }

        public Dictionary<string, TreeNode> Query(string query)
        {
            if (String.IsNullOrEmpty(language)) throw new MrKupidoException("You must set the language of the cache before requesting data.");

            return Indexer.QueryByName(query, language);
        }
    }
}
