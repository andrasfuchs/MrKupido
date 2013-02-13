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
using System.Threading;

namespace MrKupido.Processor
{
    public class RecipeCache : BaseCache
    {
        private static AppDomain dynamicDomain = null;
        private static Dictionary<string, Assembly> dynamicAssemblies = new Dictionary<string, Assembly>();
        
        public new RecipeTreeNode this [string name]
        {
            get
            {
                return (RecipeTreeNode)base[name];
            }
        }

        public RecipeTreeNode[] All
        {
            get
            {
                if (!this.WasInitialized) return new RecipeTreeNode[0];

                return Indexer.All.Cast<RecipeTreeNode>().ToArray();
            }
        }

        public void Initialize(string languageISO)
        {
            if (Indexer != null) return;

            this.language = languageISO;

            // check if the assemblies are patched already and they are up-to-date
            List<Assembly> assembliesToCheck = new List<Assembly>();
            foreach (Assembly ass in Cache.Assemblies)
            {
                if (ass.IsDynamic) continue;

                if (String.IsNullOrEmpty(ass.Location)) continue;

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
            RecipeTreeNode recipeRoot = TreeNode.BuildTree(assembliesToCheck.ToArray(), t => new RecipeTreeNode(t, languageISO), typeof(MrKupido.Library.Recipe.RecipeBase));

            // index the tree
            Indexer = new Indexer(recipeRoot, languageISO);

            WasInitialized = true;

            // this is needed for the SearchStrings property
            foreach (RecipeTreeNode rtn in Indexer.All)
            {
                if (rtn.SearchStrings.Length == 0)
                {
                    rtn.GetIngredients(1.0f, 1);
                }
            }
        }

        public RecipeTreeNode RenderInline(string masterRecipeClass, string[] inlineRecipeClasses)
        {
            RecipeTreeNode result;
            string newTypeName = masterRecipeClass + "__" + inlineRecipeClasses[0];


            // create a new domain for the dynamic assemblies
            if (dynamicDomain == null)
            {
                dynamicDomain = AppDomain.CreateDomain("DynamicAssemblies", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath, AppDomain.CurrentDomain.ShadowCopyFiles);


                dynamicDomain.AssemblyResolve += new ResolveEventHandler(dynamicDomain_AssemblyResolve);
                // NOTE: Is this a bug??
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(mainDomain_AssemblyResolve);
            }
            else
            {
                // TODO: check if we already have this generated and loaded
            }


            result = (RecipeTreeNode)Indexer.GetByClassName(masterRecipeClass);

            // keep the original assembly for the method references
            AssemblyDefinition adOrig = AssemblyDefinition.ReadAssembly(result.ClassType.Assembly.Location);

            // load assembly again, but let's modify it this time
            Guid dynamicId = Guid.NewGuid();
            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(result.ClassType.Assembly.Location);
            ad.MainModule.AssemblyReferences.Add(adOrig.Name);
            ad.Name = new AssemblyNameDefinition(ad.Name.Name + ".Dynamic." + dynamicId, ad.Name.Version);
            ad.MainModule.Name = dynamicId + ".dll";
            
            TypeDefinition td = ad.MainModule.Types.First(t => t.Name == result.ClassName);
            td.Namespace = td.Namespace + ".Dynamic";
            td.Name = newTypeName;


            // let's modify the methods
            foreach (string inlineClass in inlineRecipeClasses)
            {
                RecipeTreeNode inline = (RecipeTreeNode)Indexer.GetByClassName(inlineClass);

                // load inline type and its methods which are to be inserted
                AssemblyDefinition adInline = null;
                if (result.ClassType.Assembly.Location != inline.ClassType.Assembly.Location) AssemblyDefinition.ReadAssembly(inline.ClassType.Assembly.Location);
                else adInline = ad;

                TypeDefinition tdInline = adInline.MainModule.Types.First(t => t.Name == inlineClass);

                // TODO: do the dynamic code insertation here
                MethodDefinition md = td.Methods.First(m => m.Name == "SelectEquipment");
                MethodDefinition mdInline = tdInline.Methods.First(m => m.Name == "SelectEquipment");

                for (int i = 0; i < mdInline.Body.Instructions.Count; i++)
                {
                    md.Body.Instructions.Insert(i, mdInline.Body.Instructions[i]);
                }
            }


            // replace method references
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
            
            // remove types we don't need
            for (int i = ad.MainModule.Types.Count - 1; i >= 0; i--)
            {
                if ((ad.MainModule.Types[i].Namespace != "") && (ad.MainModule.Types[i].FullName != td.FullName)) ad.MainModule.Types.RemoveAt(i);
            }

            // save and load our new assembly
            MemoryStream ms = new MemoryStream();
            ad.Write(ms);
            //ad.Write(Path.GetTempFileName() + ".MrKupido.dll");

            //Assembly[] assembliesDynamic = dynamicDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();

            //dynamicDomain.DefineDynamicAssembly()
            Assembly ass = Assembly.Load(ms.ToArray()); // this one uses the current domain
            dynamicAssemblies.Add(ad.FullName, ass);

            //assembliesDynamic = dynamicDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();
            //assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();

            // BUG: something is just wrong here: the assembly gets loaded by the current AppDomain, not by the dynamicDomain

            ass = dynamicDomain.Load(ms.ToArray());

            //assembliesDynamic = dynamicDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();
            //assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();

            //object newTypeInstance = dynamicDomain.CreateInstanceFromAndUnwrap(ad.FullName, td.FullName);

            result = new RecipeTreeNode(ass.GetType(td.FullName), null);

            //assembliesDynamic = dynamicDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();
            //assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName).ToArray();
            
            //AppDomain.Unload(dynamicDomain);

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
            // good
            Assembly result = null;

            dynamicAssemblies.TryGetValue(args.Name, out result);

            return result;
        }

        static Assembly mainDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // NOT GOOD
            Assembly result = null;

            dynamicAssemblies.TryGetValue(args.Name, out result);

            return result;
        }
    }
}
