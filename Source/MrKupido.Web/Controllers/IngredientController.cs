using MrKupido.Processor;
using MrKupido.Processor.Model;
using System.Web.Mvc;

namespace MrKupido.Web.Controllers
{
    public class IngredientController : BaseController
    {
        //[Authorize]
        public ActionResult Trees()
        {
            TreeNode[] result = new TreeNode[5];

            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Nature.NatureBase));
            result[1] = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Ingredient.IngredientBase));
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Equipment.EquipmentBase));
            result[4] = TreeNode.BuildTree(Cache.Assemblies, t => new TagTreeNode(t, (string)Session["Language"]), typeof(MrKupido.Library.Tag.TagBase));

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