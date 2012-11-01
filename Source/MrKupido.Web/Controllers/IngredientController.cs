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

namespace MrKupido.Web.Controllers
{
    public class IngredientController : BaseController
    {
        public ActionResult Taxonomy()
        {
            object[] result = new object[4];

            result[0] = TreeNode.BuildTree(Cache.Assemblies, t => new NatureTreeNode(t), typeof(MrKupido.Library.Nature.NatureBase));
            result[1] = Cache.Ingredient["IngredientBase"];
            result[2] = TreeNode.BuildTree(Cache.Assemblies, t => new RecipeTreeNode(t), typeof(MrKupido.Library.Recipe.RecipeBase));
            result[3] = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t), typeof(MrKupido.Library.Equipment.EquipmentBase));

            return View(result);
        }
    }
}