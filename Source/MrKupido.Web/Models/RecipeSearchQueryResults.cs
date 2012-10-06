using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Processor.Model;

namespace MrKupido.Web.Models
{
    public class RecipeSearchQueryResults
    {
        public string DisplayString;
        public TreeNode TreeNode;

        public RecipeSearchQueryResults(string displayString, TreeNode treeNode)
        {
            this.DisplayString = displayString;
            this.TreeNode = treeNode;
        }
    }
}