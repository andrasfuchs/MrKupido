using System.Web.Script.Serialization;

namespace MrKupido.Processor.Model
{
    public class EquipmentCommand
    {
        public string[] Names;

        [ScriptIgnore]
        public System.Reflection.MethodInfo Method;

        public override string ToString()
        {
            return Method.Name + " (" + Names.Length + ")";
        }
    }
}
