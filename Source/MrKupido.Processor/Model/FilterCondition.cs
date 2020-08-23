namespace MrKupido.Processor.Model
{
    public class FilterCondition
    {
        public string Text;
        public string Value;
        public bool IsNeg;
        public string SearchString;
        public TreeNode Node;

        public FilterCondition(TreeNode tn, bool isNeg)
        {
            Node = tn;
            IsNeg = isNeg;
            SearchString = tn.NodeType + ":" + tn.UniqueName;
            Text = (isNeg ? '-' : '+') + " " + tn.ShortName;
            Value = (isNeg ? '-' : '+') + " " + SearchString;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}