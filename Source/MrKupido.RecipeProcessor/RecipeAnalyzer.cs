using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using Mono.Cecil;
using System.Reflection.Emit;
using System.Reflection;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class RecipeAnalyzer
    {
        private IRecipe recipe;
        private float amount;

        public RecipeAnalyzer(Type recipe, float amount)
        {
            this.recipe = Activator.CreateInstance(recipe, amount) as IRecipe;
            this.amount = amount;
        }

        public IIngredient[] GenerateIngredients()
        {
            List<IIngredient> result = new List<IIngredient>();

            EquipmentGroup eg = recipe.SelectEquipment(amount);
            PreparedIngredients preps = recipe.Prepare(amount, eg);
            CookedFoodParts cfp = recipe.Cook(amount, preps, eg);
            recipe.Serve(amount, cfp, eg);

            Type recipeType = recipe.GetType();
            MethodReference[] methodsPrep = Interception_Cecil(recipeType.Assembly.Location, recipeType.FullName, "Prepare");
            MethodReference[] methodsCook = Interception_Cecil(recipeType.Assembly.Location, recipeType.FullName, "Cook");

            return result.ToArray();
        }

        private MethodReference[] Interception_Cecil(string assemblyLocation, string classFullName, string methodName)
        {
            List<MethodReference> result = new List<MethodReference>();
            
            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(assemblyLocation);
            TypeDefinition td = ad.MainModule.Types.Where(t => t.FullName == classFullName).FirstOrDefault();
            if (td == null) throw new MrKupidoException("Mono.Cecil can't read the type definition of '{0}'.", classFullName);

            Mono.Cecil.MethodDefinition md = td.Methods.Where(m => m.Name == methodName).FirstOrDefault();
            if (md == null) throw new MrKupidoException("Mono.Cecil can't read the method definition of '{0}' on type '{1}'.", methodName, classFullName);

            MethodReference interceptionMethod = AssemblyDefinition.ReadAssembly(this.GetType().Assembly.Location).MainModule.Types.First(t => t.FullName == this.GetType().FullName).Methods.First(m => m.Name == "InterceptionMethod");
            interceptionMethod = ad.MainModule.Import(interceptionMethod);

            
            Mono.Cecil.Cil.ILProcessor processor = md.Body.GetILProcessor();
            for (int i=0; i<md.Body.Instructions.Count; i++)
            {
                Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];

                if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Newobj)
                {
                    MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;

                    Mono.Cecil.Cil.Instruction interceptionCall = processor.Create(Mono.Cecil.Cil.OpCodes.Call, interceptionMethod);

                    processor.InsertAfter(instr, interceptionCall);
                    
                    result.Add(mr);
                }
            }
            
            ad.Write(@"c:\Work\MrKupido\Source\MrKupido.Library.Recipe\bin\Debug\MrKupido.Library.Recipe.patched.dll");

            return result.ToArray();
        }

        private DynamicMethod Interception_Reflection(MethodInfo method)
        {
            DynamicMethod result = new DynamicMethod(method.Name, method.ReturnType, method.GetParameters().Select(p => p.ParameterType).ToArray());

            MethodInfo interceptionMethod = this.GetType().GetMethod("InterceptionMethod", BindingFlags.Static);
            //interceptionMethod = ad.MainModule.Import(interceptionMethod);

            ILGenerator il = result.GetILGenerator(256);

            result = DynamicMethodHelper.ConvertFrom(method);

            //method.GetMethodBody().
            //for (int i = 0; i < md.Body.Instructions.Count; i++)
            //{
            //    Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];

            //    if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Newobj)
            //    {
            //        MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;

            //        Mono.Cecil.Cil.Instruction interceptionCall = processor.Create(Mono.Cecil.Cil.OpCodes.Call, interceptionMethod);

            //        processor.InsertAfter(instr, interceptionCall);

            //        result.Add(mr);
            //    }
            //}

            return result;
        }

        public static object InterceptionMethod(object returnedObject)
        {
            return returnedObject;
        }

        public Instruction[] GenerateIntructions()
        {
            List<Instruction> result = new List<Instruction>();

            return result.ToArray();
        }

        public IEquipment[] EquipmentNeeded()
        {
            List<IEquipment> result = new List<IEquipment>();

            return result.ToArray();
        }

        public string[] Nutritions()
        {
            List<string> result = new List<string>();

            return result.ToArray();
        }
    }
}
