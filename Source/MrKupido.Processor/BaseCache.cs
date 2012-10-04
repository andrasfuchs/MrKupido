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
        public bool WasInitialized = false;

        protected Indexer ri;

        public TreeNode this[string name]
        {
            get
            {
                TreeNode result = null;

                if (result == null)
                {
                    result = ri.GetByUniqueName(name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
                }

                if (result == null)
                {
                    result = ri.GetByClassName(name);
                }

                if (result == null)
                {
                    result = ri.GetByName(name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
                }

                return result;
            }
        }

        public Dictionary<string, TreeNode> Query(string query)
        {
            return ri.QueryByName(query, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
        }
    }
}
