using MrKupido.Processor;
using MrKupido.Processor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MrKupido.Web.Core.Controllers
{
    public class IngredientController : BaseController
    {
        //[Authorize]
        public ActionResult Trees()
        {
            string sessionLanguage = HttpContext.Session.GetString("Language");

            TreeNode[] result = new TreeNode[5];

            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t, sessionLanguage), typeof(MrKupido.Library.Nature.NatureBase));
            result[1] = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t, sessionLanguage), typeof(MrKupido.Library.Ingredient.IngredientBase));
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t, sessionLanguage), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t, sessionLanguage), typeof(MrKupido.Library.Equipment.EquipmentBase));
            result[4] = TreeNode.BuildTree(Cache.Assemblies, t => new TagTreeNode(t, sessionLanguage), typeof(MrKupido.Library.Tag.TagBase));

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