using Mono.Cecil;
using MrKupido.Library;
using System;
using System.Linq;

namespace MrKupido.Processor.Model
{
    [Obsolete]
    public class RecipeMethod
    {
        public System.Reflection.MethodInfo Original { get; private set; }
        public Mono.Cecil.Cil.Instruction[] ILCode { get; private set; }
        public System.Reflection.MethodInfo Patched { get; private set; }
        [Obsolete]
        public System.Reflection.Emit.DynamicMethod PatchedDM { get; private set; }

        public RecipeMethod(Type type, Type patchedType, TypeDefinition td, string methodName)
        {
            Original = type.GetMethod(methodName);

            MethodDefinition md = td.Methods.Where(m => m.Name == methodName).FirstOrDefault();
            if (md == null) throw new MrKupidoException("Mono.Cecil can't read the method definition of '{0}' on type '{1}'.", methodName, type.FullName);

            ILCode = md.Body.Instructions.ToArray();

            Patched = patchedType.GetMethod(methodName);
            PatchedDM = DynamicMethodHelper.ConvertFrom(Patched);
        }

    }
}
