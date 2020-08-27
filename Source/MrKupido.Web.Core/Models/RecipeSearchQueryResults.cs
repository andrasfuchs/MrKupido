using MrKupido.Processor.Model;

namespace MrKupido.Web.Core.Models
{
    public class RecipeSearchQueryResults
    {
        public string DisplayString;
        public string SearchString;
        public string UniqueName;
        public char NodeType;
        public string IconUrl;

        public RecipeSearchQueryResults() { }

        public RecipeSearchQueryResults(string displayString, TreeNode treeNode, string rootUrl)
        {
            this.DisplayString = displayString;
            this.SearchString = displayString;
            this.UniqueName = treeNode.UniqueName;
            this.NodeType = treeNode.NodeType;
            this.IconUrl = treeNode.GetIconUrl(rootUrl);
        }
    }
}