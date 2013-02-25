using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Model;
using MrKupido.DataAccess;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using MrKupido.Library;
using MrKupido.Processor.Model;
using MrKupido.Processor;
using MrKupido.Utils;

namespace MrKupido.Web.Controllers
{
    public class IngredientController : BaseController
    {
        //[Authorize]
        public ActionResult Trees()
        {
            TreeNode[] result = new TreeNode[4];

            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Nature.NatureBase));
            //result[1] = Cache.Ingredient["MrKupido.Library.Ingredient.IngredientBase"];
            result[1] = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Ingredient.IngredientBase));
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Equipment.EquipmentBase));

            ValidateIconUrls(result);

            return View(result);
        }

        private void ValidateIconUrls(TreeNode[] tns)
        {
            if (tns == null) return;

            foreach (TreeNode tn in tns)
            {
                ValidateIconUrls(tn.Children);
            }
        }
    }
}