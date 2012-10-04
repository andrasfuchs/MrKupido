using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Processor.Model;

namespace MrKupido.Web.Models
{
    public class SearchQueryResult
    {
        public string DisplayString;
        public TreeNode TreeNode;

        public SearchQueryResult(string displayString, TreeNode treeNode)
        {
            this.DisplayString = displayString;
            this.TreeNode = treeNode;
        }
    }
}