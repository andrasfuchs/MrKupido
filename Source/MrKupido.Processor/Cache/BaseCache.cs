using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using System.Threading;

namespace MrKupido.Processor
{
    public class BaseCache
    {
        public Indexer Indexer { get; protected set; }

        public bool WasInitialized = false;

        public TreeNode this[string name]
        {
            get
            {
                TreeNode result = null;

                if (result == null)
                {
                    result = Indexer.GetByUniqueName(name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
                }

                if (result == null)
                {
                    result = Indexer.GetByClassName(name);
                }

                if (result == null)
                {
                    result = Indexer.GetByName(name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
                }

                return result;
            }
        }

        public Dictionary<string, TreeNode> Query(string query)
        {
            return Indexer.QueryByName(query, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
        }
    }
}
