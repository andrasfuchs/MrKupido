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
        public ActionResult Taxonomy()
        {
            TreeNode[] result = new TreeNode[4];

            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t, System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName), typeof(MrKupido.Library.Nature.NatureBase));
            result[1] = Cache.Ingredient["IngredientBase"];
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t, System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t, System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName), typeof(MrKupido.Library.Equipment.EquipmentBase));

            ValidateIconUrls(result);
            //ValidateIconUrls(new TreeNode[] { result[0] });
            //ValidateIconUrls(new TreeNode[] { result[1] });
            //ValidateIconUrls(new TreeNode[] { result[2] });
            //ValidateIconUrls(new TreeNode[] { result[3] });

            return View(result);
        }

        private void ValidateIconUrls(TreeNode[] tns)
        {
            if (tns == null) return;

            foreach (TreeNode tn in tns)
            {
                if (tn.IconUrl == null) tn.IconUrl = PathUtils.GetActualUrl(tn.IconUrls);
                tn.IconUrl = tn.IconUrl;

                ValidateIconUrls(tn.Children);
            }
        }
    }
}