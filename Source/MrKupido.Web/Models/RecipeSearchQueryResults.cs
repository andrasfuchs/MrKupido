using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Processor.Model;
using MrKupido.Utils;

namespace MrKupido.Web.Models
{
    public class RecipeSearchQueryResults
    {
        public string DisplayString;
        public string SearchString;
        public string UniqueName;
        public char NodeType;
        public string IconUrl;

        public RecipeSearchQueryResults() { }

        public RecipeSearchQueryResults(string displayString, TreeNode treeNode)
        {
            this.DisplayString = displayString;
            this.SearchString = displayString;
            this.UniqueName = treeNode.UniqueName;
            this.NodeType = treeNode.NodeType;
            if (treeNode.IconUrl == null) treeNode.IconUrl = PathUtils.GetActualUrl(treeNode.IconUrls);
            this.IconUrl = treeNode.IconUrl;
        }
    }
}