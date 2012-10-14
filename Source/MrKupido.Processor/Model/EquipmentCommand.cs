using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
