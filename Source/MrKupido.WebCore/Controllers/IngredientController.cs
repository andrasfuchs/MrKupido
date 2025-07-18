using MrKupido.Processor;
using MrKupido.Processor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MrKupido.Web.Controllers
{
    public class IngredientController : BaseController
    {
        //[Authorize]
        public IActionResult Trees()
        {
            TreeNode[] result = new TreeNode[5];
            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t, HttpContext.Session.GetString("Language")), typeof(MrKupido.Library.Nature.NatureBase));
            result[1] = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t, HttpContext.Session.GetString("Language")), typeof(MrKupido.Library.Ingredient.IngredientBase));
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t, HttpContext.Session.GetString("Language")), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t, HttpContext.Session.GetString("Language")), typeof(MrKupido.Library.Equipment.EquipmentBase));
            result[4] = TreeNode.BuildTree(Cache.Assemblies, t => new TagTreeNode(t, HttpContext.Session.GetString("Language")), typeof(MrKupido.Library.Tag.TagBase));

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