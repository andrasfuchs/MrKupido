using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MrKupido.Processor.Model
{
    public class EquipmentTreeNode : TreeNode
    {
        public EquipmentCommand[] ValidCommands { private set; get; }

        public EquipmentTreeNode(Type equipmentClass, string languageISO)
            : base(equipmentClass, languageISO)
        {
            List<EquipmentCommand> commands = new List<EquipmentCommand>();

            foreach (MethodInfo mi in equipmentClass.GetMethods())
            {
                if (mi.Name.StartsWith("get_")) continue;
                if (mi.Name.StartsWith("set_")) continue;

                if ((mi.Name == "ToString") || (mi.Name == "Equals") || (mi.Name == "GetHashCode") || (mi.Name == "GetType")) continue;

                EquipmentCommand ec = new EquipmentCommand();

                ec.Method = mi;
                ec.Names = NameAliasAttribute.GetNames(equipmentClass, mi.Name).Select(att => att.Name).ToArray();

                commands.Add(ec);
            }

            ValidCommands = commands.ToArray();
        }
    }
}
