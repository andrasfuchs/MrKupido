using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.Diagnostics;
using System.Reflection;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using System.Web.Script.Serialization;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Processor.Model
{
    public class TagTreeNode : TreeNode
    {
		public TagTreeNode(Type tagClass, string languageISO)
			: base(tagClass, languageISO)
		{ 
		}
    }
}
