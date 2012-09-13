using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Mono.Cecil;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class RecipeCache
    {
        private Indexer ri;
        
        public RecipeTreeNode this [string className]
        {
            get
            {
                return (RecipeTreeNode)ri.GetByClassName(className);
            }
        }

        public void Initialize(Type interceptionType, string constructorMethodName)
        {
            // check if the assemblies are patched already and they are up-to-date
            List<Assembly> assembliesToCheck = new List<Assembly>();
            foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                if ((ass.IsDynamic) || (ass.Location.EndsWith(".Patched.dll"))) continue;

                Type recipe = ass.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(MrKupido.Library.Recipe.RecipeBase)));
                if (recipe != null)
                {
                    assembliesToCheck.Add(ass);

                    string patchFilename = ass.Location + ".Patched.dll";
                    if (!File.Exists(patchFilename) || (File.GetCreationTime(patchFilename) < File.GetCreationTime(ass.Location)))
                    {
                        PatchAssembly(ass, patchFilename, interceptionType, constructorMethodName);
                    }
                }
            }

            // build the recipe tree
            RecipeTreeNode recipeRoot = TreeNode.BuildTree(assembliesToCheck.ToArray(), t => new RecipeTreeNode(t), typeof(MrKupido.Library.Recipe.RecipeBase));

            // index the tree
            ri = new Indexer(recipeRoot);
        }

        private void PatchAssembly(Assembly ass, string patchFilename, Type interceptionType, string constructorMethodName)
        {
            // collect all the classes which are descendants of RecipeBase
            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(ass.Location);
            foreach (TypeDefinition td in ad.MainModule.Types)
            {
                if ((td.FullName == "<Module>") || (!ass.GetType(td.FullName).IsSubclassOf(typeof(MrKupido.Library.Recipe.RecipeBase)))) continue;

                // do the patching for the known methods
                foreach (MethodDefinition md in td.Methods)
                {
                    PatchMethod(md, interceptionType, constructorMethodName);
                }
            }
            ad.Write(patchFilename);
        }

        private void PatchMethod(MethodDefinition md, Type interceptionType, string constructorMethodName)
        {
            MethodReference interceptionMethod = AssemblyDefinition.ReadAssembly(interceptionType.Assembly.Location).MainModule.Types.First(t => t.FullName == interceptionType.FullName).Methods.First(m => m.Name == constructorMethodName);
            interceptionMethod = md.Module.Import(interceptionMethod);


            Mono.Cecil.Cil.ILProcessor processor = md.Body.GetILProcessor();
            for (int i = 0; i < md.Body.Instructions.Count; i++)
            {
                Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];

                if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Newobj)
                {
                    MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;

                    Mono.Cecil.Cil.Instruction interceptionCall = processor.Create(Mono.Cecil.Cil.OpCodes.Call, interceptionMethod);

                    processor.InsertAfter(instr, interceptionCall);
                }
            }
        }
    }
}
