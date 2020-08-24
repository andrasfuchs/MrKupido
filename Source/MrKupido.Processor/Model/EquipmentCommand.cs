using System.Web.Script.Serialization;
using System.Text.Json.Serialization;

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
