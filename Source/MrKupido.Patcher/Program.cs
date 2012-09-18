using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.IO;

namespace MrKupido.Patcher
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: mrkupido.patcher.exe <dll to patch> <interception dll> <interception type name> <constructor method name>");
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
            MethodDefinition interMD = interTD.Methods.FirstOrDefault(m => m.Name == args[3]);

            Console.Write("Patching...");
            PatchAssembly(patchAD, interMD);
            patchInfo.Fields[0].Constant = File.GetLastWriteTime(args[0]).ToString("yyyy-MM-dd HH:mm:ss");

            if (File.Exists(args[0] + ".orig.dll")) File.Delete(args[0] + ".orig.dll");
            File.Move(args[0], args[0] + ".orig.dll");
            patchAD.Write(args[0]);
            Console.WriteLine("done!");
            Console.ReadKey();

            return 0;
        }

        private static void PatchAssembly(AssemblyDefinition patchAD, MethodDefinition interMD)
        {
            // collect all the classes which are descendants of RecipeBase
            foreach (TypeDefinition td in patchAD.MainModule.Types)
            {
                if (td.FullName == "<Module>") continue;

                // do the patching for the known methods
                foreach (MethodDefinition md in td.Methods)
                {
                    PatchMethod(md, interMD);
                }
            }
        }

        private static void PatchMethod(MethodDefinition md, MethodReference interMD)
        {
            interMD = md.Module.Import(interMD);

            Mono.Cecil.Cil.ILProcessor processor = md.Body.GetILProcessor();
            for (int i = 0; i < md.Body.Instructions.Count; i++)
            {
                Mono.Cecil.Cil.Instruction instr = md.Body.Instructions[i];

                if (instr.OpCode.Code == Mono.Cecil.Cil.Code.Newobj)
                {
                    MethodReference mr = (Mono.Cecil.MethodReference)instr.Operand;

                    Mono.Cecil.Cil.Instruction interceptionCall = processor.Create(Mono.Cecil.Cil.OpCodes.Call, interMD);

                    processor.InsertAfter(instr, interceptionCall);
                }
            }
        }

    }
}
