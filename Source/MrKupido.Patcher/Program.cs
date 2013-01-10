using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace MrKupido.Patcher
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Usage: mrkupido.patcher.exe <dll to patch> <interception dll> <interception type name> <new ingredient method name> <direction generator method name>");
                Console.ReadKey();
                return -1;
            }

            AssemblyDefinition patchAD = AssemblyDefinition.ReadAssembly(args[0]);
            if (patchAD == null)
            {
                Console.WriteLine("The dll to patch '{0}' could not be loaded.", args[0]);
                return -2;
            }

            TypeDefinition patchInfo = patchAD.MainModule.GetType("MrKupido.Library.Recipe.PatchInfo");
            if (patchInfo == null)
            {
                Console.WriteLine("The dll to patch has no 'PatchInfo' class which is needed for the patching process.");
                return -3;
            }

            if (patchInfo.Fields[0].Name != "PatchedVersion")
            {
                Console.WriteLine("The dll to patch has 'PatchInfo' class but it must has a constant field called 'PatchedVersion'.");
                return -4;
            }

            DateTime patchedAssemblyTime = DateTime.ParseExact((string)patchInfo.Fields[0].Constant, "yyyy-MM-dd HH:mm:ss", null);

            if (patchedAssemblyTime != DateTime.MinValue)
            {
                Console.WriteLine("The dll to patch has been already patched. The modification time of the original dll is '{0}'.", patchedAssemblyTime.ToString("yyyy-MM-dd HH:mm:ss"));
                return -5;
            }

            AssemblyDefinition interAD = AssemblyDefinition.ReadAssembly(args[1]);
            TypeDefinition interTD = interAD.MainModule.GetType(args[2]);
            if (interTD == null)
            {
                Console.WriteLine("The type '{0}' was not found in assembly '{1}'.", args[2], args[1]);
                return -6;
            }

            MethodDefinition newIngredientMD = interTD.Methods.FirstOrDefault(m => m.Name == args[3]);
            if (newIngredientMD == null)
            {
                Console.WriteLine("The method '{0}' was not found in type '{1}' and assembly '{2}'.", args[3], args[2], args[1]);
                return -7;
            }

            MethodDefinition directionGeneratorMD = interTD.Methods.FirstOrDefault(m => m.Name == args[4]);
            if (directionGeneratorMD == null)
            {
                Console.WriteLine("The method '{0}' was not found in type '{1}' and assembly '{2}'.", args[4], args[2], args[1]);
                return -8;
            }


            Console.Write("Patching...");
            PatchAssembly(patchAD, newIngredientMD, directionGeneratorMD);
            patchInfo.Fields[0].Constant = File.GetLastWriteTime(args[0]).ToString("yyyy-MM-dd HH:mm:ss");

            if (File.Exists(args[0] + ".orig.dll")) File.Delete(args[0] + ".orig.dll");
            File.Move(args[0], args[0] + ".orig.dll");
            patchAD.Write(args[0]);
            Console.WriteLine("done!");
            //Console.ReadKey();

            return 0;
        }

        private static void PatchAssembly(AssemblyDefinition patchAD, MethodDefinition newIngredientMD, MethodReference directionGeneratorMD)
        {
            // collect all the classes which are descendants of RecipeBase
            foreach (TypeDefinition td in patchAD.MainModule.Types)
            {
                if (td.FullName == "<Module>") continue;

                // do the patching for the known methods
                foreach (MethodDefinition md in td.Methods)
                //foreach (MethodDefinition md in td.Methods.Where(md => md.DeclaringType.Name == "FuszeresCsirkemell"))
                {
                    PatchMethod(md, newIngredientMD, directionGeneratorMD);
                }
            }
        }

        private static void PatchMethod(MethodDefinition md, MethodReference newIngredientMD, MethodReference directionGeneratorMD)
        {
            newIngredientMD = md.Module.Import(newIngredientMD);
            directionGeneratorMD = md.Module.Import(directionGeneratorMD);

            md.Body.SimplifyMacros();

            Mono.Cecil.Cil.ILProcessor processor = md.Body.GetILProcessor();
            for (int i = 0; i < md.Body.Instructions.Count; i++)
            {
                Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];
                Mono.Cecil.Cil.Instruction instrNext = instr.Next;

                if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Newobj)
                {
                    MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;

                    if ((mr.DeclaringType.Namespace == "MrKupido.Library.Ingredient") || (mr.DeclaringType.Namespace == "MrKupido.Library.Recipe"))
                    {
                        Mono.Cecil.Cil.Instruction interceptionCall = processor.Create(Mono.Cecil.Cil.OpCodes.Call, newIngredientMD);

                        processor.InsertAfter(instr, interceptionCall);
                    }
                }

                #region Direction Generator
                if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Callvirt)
                {
                    MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;
                    Mono.Cecil.Cil.Instruction ii = null;

                    if ((mr.DeclaringType.Namespace == "MrKupido.Library.Equipment") && (!mr.Name.StartsWith("get_")) && (!mr.Name.StartsWith("set_")) && (mr.HasThis))
                    {
                        Mono.Cecil.Cil.VariableDefinition[] nvs = new Mono.Cecil.Cil.VariableDefinition[mr.Parameters.Count + 1];

                        // instance variable
                        nvs[0] = new Mono.Cecil.Cil.VariableDefinition(mr.DeclaringType);
                        md.Body.Variables.Add(nvs[0]);

                        // parameters
                        for (int j = mr.Parameters.Count - 1; j >= 0; j--)
                        {
                            nvs[j+1] = new Mono.Cecil.Cil.VariableDefinition(mr.Parameters[j].ParameterType);
                            md.Body.Variables.Add(nvs[j + 1]);

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Stloc_S, nvs[j + 1]);
                            processor.InsertBefore(instr, ii);
                        }

                        // instance
                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Stloc_S, nvs[0]);
                        processor.InsertBefore(instr, ii);
                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, nvs[0]);
                        processor.InsertBefore(instr, ii);

                        // parameters
                        for (int j = 0; j < mr.Parameters.Count; j++)
                        {
                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, nvs[j + 1]);
                            processor.InsertBefore(instr, ii);
                        }

                        i = md.Body.Instructions.IndexOf(instr);


                        Mono.Cecil.Cil.VariableDefinition result = null;

                        if (mr.ReturnType.FullName != "System.Void")
                        {
                            // we need to store the return value
                            result = new Mono.Cecil.Cil.VariableDefinition(mr.ReturnType);
                            md.Body.Variables.Add(result);

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Stloc_S, result);
                            processor.InsertBefore(instrNext, ii);

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, result);
                            processor.InsertBefore(instrNext, ii);
                        }

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldc_I4, nvs.Length);
                        processor.InsertBefore(instrNext, ii);

                        TypeReference objectTypeRef = directionGeneratorMD.Parameters[3].ParameterType.GetElementType(); // this MUST reference the 'object' type

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Newarr, objectTypeRef);
                        processor.InsertBefore(instrNext, ii);

                        Mono.Cecil.Cil.VariableDefinition arrayVar = new Mono.Cecil.Cil.VariableDefinition(objectTypeRef);
                        md.Body.Variables.Add(arrayVar);

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Stloc_S, arrayVar);
                        processor.InsertBefore(instrNext, ii);


                        for (int j = 0; j < nvs.Length; j++)
                        {
                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, arrayVar);
                            processor.InsertBefore(instrNext, ii);

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldc_I4, j);
                            processor.InsertBefore(instrNext, ii);

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, nvs[j]);
                            processor.InsertBefore(instrNext, ii);

                            // do the boxing if necessary
                            if (nvs[j].VariableType.IsValueType)
                            {
                                ii = processor.Create(Mono.Cecil.Cil.OpCodes.Box, nvs[j].VariableType);
                                processor.InsertBefore(instrNext, ii);
                            }

                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Stelem_Ref);
                            processor.InsertBefore(instrNext, ii);
                        }

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldstr, mr.Module.AssemblyResolver.Resolve(mr.DeclaringType.Scope.Name).FullName);
                        processor.InsertBefore(instrNext, ii);

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldstr, mr.DeclaringType.FullName + " " + mr.Name);
                        processor.InsertBefore(instrNext, ii);

                        // this is the good one
                        //ii = processor.Create(instr.Previous.OpCode, md.Body.Variables[((Mono.Cecil.Cil.VariableReference)instr.Previous.Operand).Index]);
                        //ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc, md.Body.Variables[0]);
                        //processor.InsertBefore(instrNext, ii);
                        //MethodReference mref = new MethodReference("get_LastActionDuration", directionGeneratorMD.Parameters[2].ParameterType, new TypeReference(mr.DeclaringType.Namespace, "EquipmentBase", mr.DeclaringType.Module, mr.DeclaringType.Scope));
                        //mref.HasThis = true;
                        //ii = processor.Create(instr.OpCode, mref);
                        //processor.InsertBefore(instrNext, ii);

                        // not good, it is a static call (we need virtual instance)
                        //ii = processor.Create(instr.OpCode, new MethodReference("get_LastActionDuration", directionGeneratorMD.Parameters[2].ParameterType, new TypeReference(mr.DeclaringType.Namespace, "EquipmentBase", mr.DeclaringType.Module, mr.DeclaringType.Scope)));
                        //processor.InsertBefore(instrNext, ii);

                        // not good, temporary replacement
                        //ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldc_I4, 60);
                        //processor.InsertBefore(instrNext, ii);

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, arrayVar);
                        processor.InsertBefore(instrNext, ii);

                        if (mr.ReturnType.FullName != "System.Void")
                        {
                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldloc_S, result);
                            processor.InsertBefore(instrNext, ii);

                            // do the boxing if necessary
                            if (result.VariableType.IsValueType)
                            {
                                ii = processor.Create(Mono.Cecil.Cil.OpCodes.Box, result.VariableType);
                                processor.InsertBefore(instrNext, ii);
                            }
                        }
                        else
                        {
                            ii = processor.Create(Mono.Cecil.Cil.OpCodes.Ldnull);
                            processor.InsertBefore(instrNext, ii);
                        }

                        ii = processor.Create(Mono.Cecil.Cil.OpCodes.Call, directionGeneratorMD);
                        processor.InsertBefore(instrNext, ii);

                        i = md.Body.Instructions.IndexOf(instrNext) - 1;
                    }
                }
                #endregion

            }

            md.Body.OptimizeMacros();


            // TODO: take care of branches location
            //for (int i = 0; i < md.Body.Instructions.Count; i++)
            //{
            //    Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];

            //    if (instr.Operand is Mono.Cecil.Cil.Instruction)
            //    {
            //        Mono.Cecil.Cil.Instruction ir = (Mono.Cecil.Cil.Instruction)instr.Operand;
            //    }
            //}
        }

    }
}
