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

        public T Use<T>() where T : EquipmentBase
        {
            T result = null;
            string typeFullName = typeof(T).FullName;
            IEnumerable<IEquipment> sametypeEquipment;

            if (typeof(T).CheckParents(typeof(Container), false))
            {
                sametypeEquipment = Containers.Where(c => c.GetType().FullName == typeFullName);
            }
            else if (typeof(T).CheckParents(typeof(Device), false))
            {
                sametypeEquipment = Devices.Where(c => c.GetType().FullName == typeFullName);
            }
            else if (typeof(T).CheckParents(typeof(Material), false))
            {
                sametypeEquipment = Materials.Where(c => c.GetType().FullName == typeFullName);
            }
            else if (typeof(T).CheckParents(typeof(Tool), false))
            {
                sametypeEquipment = Tools.Where(c => c.GetType().FullName == typeFullName);
            }
            else
            {
                throw new NotImplementedException();
            }

            if (sametypeEquipment.Count() == 0)
            {
                throw new MrKupidoException("There are no classes of type '{0}' in the equipment group. Please add it to the 'SelectEquipment' method of the recipe.", typeFullName);
            }

            result = sametypeEquipment.FirstOrDefault(c => !c.IsInUse) as T;

            if (result == null)
            {
                throw new MrKupidoException("All the equipment of type '{0}' in the equipment group are in use.", typeFullName);
            }

            result.Use();

            return result;
        }

        public void WashUp()
        {
            foreach (Container container in Containers)
            {
                if (container.IsInUse) container.WashUp();
            }
            foreach (Device device in Devices)
            {
                if (device.IsInUse) device.WashUp();
            }
            foreach (Material material in Materials)
            {
                if (material.IsInUse) material.WashUp();
            }
            foreach (Tool tool in Tools)
            {
                if (tool.IsInUse) tool.WashUp();
            }
        }
    }
}
