using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using System.Threading;
using MrKupido.Library;

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
