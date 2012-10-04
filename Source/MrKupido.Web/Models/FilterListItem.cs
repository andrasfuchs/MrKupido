using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Processor.Model;

namespace MrKupido.Web.Models
{
    public class FilterListItem
    {
        public string Text;
        public string Value;
        public bool IsNeg;
        //public TreeNode TN;

        public FilterListItem(TreeNode tn, bool isNeg)
        {
            //TN = tn;
            IsNeg = isNeg;
            Text = (isNeg ? '-' : '+') + " " + tn.ShortName;
            Value = (isNeg ? '-' : '+') + " " + tn.NodeType + ":" + tn.UniqueName;
        }
    }
}