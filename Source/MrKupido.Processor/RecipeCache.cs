using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Mono.Cecil;
using MrKupido.Processor.Model;
using System.Diagnostics;
using MrKupido.Library;
using System.Reflection.Emit;

namespace MrKupido.Processor
{
    public class RecipeCache
    {
        private Indexer ri;
        public static AppDomain dynamicDomain = null;
        
        public RecipeTreeNode this [string className]
        {
            get
            {
                return (RecipeTreeNode)ri.GetByClassName(className);
            }
        }

        public void Initialize()
        {
            // check if the assemblies are patched already and they are up-to-date
            List<Assembly> assembliesToCheck = new List<Assembly>();
            foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (ass.IsDynamic) continue;

                if (ass.GetTypes().Where(t => (t.FullName == "MrKupido.Library.Recipe.RecipeBase") || t.IsSubclassOf(typeof(MrKupido.Library.Recipe.RecipeBase))).Count() > 0) assembliesToCheck.Add(ass);

                AssemblyDefinition patchAD = AssemblyDefinition.ReadAssembly(ass.Location);
                TypeDefinition patchInfo = patchAD.MainModule.GetType("MrKupido.Library.Recipe.PatchInfo");
                if (patchInfo == null) continue;
                if (patchInfo.Fields[0].Name != "PatchedVersion") continue;
                DateTime patchedAssemblyTime = DateTime.ParseExact((string)patchInfo.Fields[0].Constant, "yyyy-MM-dd HH:mm:ss", null);

                if (patchedAssemblyTime == DateTime.MinValue)
                {
                    Trace.TraceWarning("The assembly '{0}' is not patched. Some functionality in the system may not work. Consider running the patcher on the dll.", ass.Location);
                }
            }

            // build the recipe tree
            RecipeTreeNode recipeRoot = TreeNode.BuildTree(assembliesToCheck.ToArray(), t => new RecipeTreeNode(t), typeof(MrKupido.Library.Recipe.RecipeBase));

            // index the tree
            ri = new Indexer(recipeRoot);
        }

        public RecipeTreeNode RenderInline(string masterRecipeClass, string[] inlineRecipeClasses)
        {
            RecipeTreeNode result;

            if (dynamicDomain == null)
            {
                dynamicDomain = AppDomain.CreateDomain("DynamicAssemblies", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath, AppDomain.CurrentDomain.ShadowCopyFiles);

                dynamicDomain.AssemblyResolve += new ResolveEventHandler(dynamicDomain_AssemblyResolve);
            }

            result = (RecipeTreeNode)ri.GetByClassName(masterRecipeClass);

            Guid dynamicId = Guid.NewGuid();

            //
            AssemblyDefinition adOrig = AssemblyDefinition.ReadAssembly(result.ClassType.Assembly.Location);

            // load main method
            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(result.ClassType.Assembly.Location);
            ad.MainModule.AssemblyReferences.Add(adOrig.Name);
            ad.Name = new AssemblyNameDefinition(ad.Name.Name + ".Dynamic." + dynamicId, ad.Name.Version);
            ad.MainModule.Name = dynamicId + ".dll";
            
            TypeDefinition td = ad.MainModule.Types.First(t => t.Name == result.ClassName);
            td.Namespace = td.Namespace + ".Dynamic";
            td.Name = td.Name + "__" + inlineRecipeClasses[0];
           
            MethodDefinition md = td.Methods.First(m => m.Name == "SelectEquipment");

            foreach (string inlineClass in inlineRecipeClasses)
            {
                RecipeTreeNode inline = (RecipeTreeNode)ri.GetByClassName(inlineClass);

                // load method to be inserted
                AssemblyDefinition adInline = null;
                if (result.ClassType.Assembly.Location != inline.ClassType.Assembly.Location) AssemblyDefinition.ReadAssembly(inline.ClassType.Assembly.Location);
                else adInline = ad;

                TypeDefinition tdInline = adInline.MainModule.Types.First(t => t.Name == inlineClass);
                MethodDefinition mdInline = tdInline.Methods.First(m => m.Name == "SelectEquipment");

                for (int i = 0; i < mdInline.Body.Instructions.Count; i++)
                {
                    md.Body.Instructions.Insert(i, mdInline.Body.Instructions[i]);
                }
            }



            foreach (MethodDefinition mds in td.Methods)
            {
                foreach (Mono.Cecil.Cil.Instruction ins in mds.Body.Instructions)
                {
                    if (ins.Operand is MethodReference)
                    {
                        MethodReference mr = (MethodReference)ins.Operand;
                        TypeDefinition replacableType = adOrig.MainModule.Types.FirstOrDefault(tdef => tdef.FullName == mr.DeclaringType.FullName);

                        if (replacableType != null)
                        {
                            mr = replacableType.Methods.First(mdef => mdef.FullName == mr.FullName);
                            ins.Operand = ad.MainModule.Import(mr);
                        }
                    }
                }
            }
            
            for (int i = ad.MainModule.Types.Count - 1; i >= 0; i--)
            {
                if ((ad.MainModule.Types[i].Namespace != "") && (ad.MainModule.Types[i].FullName != td.FullName)) ad.MainModule.Types.RemoveAt(i);
            }

            MemoryStream ms = new MemoryStream();
            ad.Write(ms);
            ad.Write(Path.GetTempFileName() + ".MrKupido.dll");


            //Assembly ass = Assembly.Load(ms.ToArray());
            //dynamicAssemblies.Add(Assembly.Load(ms.ToArray()));

            //result = new RecipeTreeNode(AppDomain.CurrentDomain.Load(ms.ToArray()).GetType(td.FullName));

            result = new RecipeTreeNode(dynamicDomain.Load(ms.ToArray()).GetType(td.FullName));

            return result;

            //Type newType = dynamicDomain.Load(ms.ToArray()).GetType(td.FullName);
            //DynamicMethod resultDM = DynamicMethodHelper.ConvertFrom(newType.GetMethod("SelectEquipment"));

            //MrKupido.Library.IIngredient sajt = new Sajt(1.0f);

            float amount = 1.0f; // calculate this based on the parameter of the construtor of the ingredient class

            // merge into SelectEquipment method (use the EG's merge method)
            EquipmentGroup eq = Cache.Recipe["Sajt"].SelectEquipment(1.0f);

            // merge into the Prepare method
            PreparedIngredients preps = Cache.Recipe["Sajt"].Prepare(amount, eq);

            // merge into the cook method
            CookedFoodParts cps = Cache.Recipe["Sajt"].Cook(amount, preps, eq);
            
            // replace the original constructor of the ingredient class with the following
            MrKupido.Library.IIngredient sajt = cps[0];

            return result;
        }

        static Assembly dynamicDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
