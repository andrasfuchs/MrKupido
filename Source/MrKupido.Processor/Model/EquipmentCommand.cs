using System.Text.Json.Serialization;

namespace MrKupido.Processor.Model
{
    public class EquipmentCommand
    {
        public string[] Names { get; set; }

        [JsonIgnore]
        public System.Reflection.MethodInfo Method { get; set; }

        public override string ToString()
        {
            return Method.Name + " (" + Names.Length + ")";
        }
    }
}
