using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Equipment;

namespace MrKupido.Library
{
    public class EquipmentGroup
    {
        public List<Container> Containers { get; set; }
        public List<Device> Devices { get; set; }
        public List<Material> Materials { get; set; }
        public List<Tool> Tools { get; set; }

        public EquipmentGroup()
        {
            Containers = new List<Container>();
            Devices = new List<Device>();
            Materials = new List<Material>();
            Tools = new List<Tool>();
        }

        public T Use<T>()
        {
            throw new NotImplementedException();
        }

        public void WashUp(IEquipment eq)
        {
            throw new NotImplementedException();
        }
    }
}
