using System.Web.Script.Serialization;

namespace MrKupido.Processor.Model
{
    public class EquipmentCommand
    {
        public string[] Names { get; set; }

        [ScriptIgnore]
        public System.Reflection.MethodInfo Method { get; set; }

        public override string ToString()
        {
            return Method.Name + " (" + Names.Length + ")";
        }
    }
}
