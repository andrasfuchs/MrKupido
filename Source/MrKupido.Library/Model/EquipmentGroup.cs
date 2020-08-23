using MrKupido.Library.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public T Use<T>(int index = 0) where T : EquipmentBase
        {
            T result = null;
            string typeFullName = typeof(T).FullName;
            IEnumerable<IEquipment> sametypeEquipment;

            if (typeof(T).CheckParents(typeof(Container), false))
            {
                sametypeEquipment = Containers.Where(c => (c.GetType().FullName == typeFullName) && (c.Index == index));
            }
            else if (typeof(T).CheckParents(typeof(Device), false))
            {
                sametypeEquipment = Devices.Where(c => (c.GetType().FullName == typeFullName) && (c.Index == index));
            }
            else if (typeof(T).CheckParents(typeof(Material), false))
            {
                sametypeEquipment = Materials.Where(c => (c.GetType().FullName == typeFullName) && (c.Index == index));
            }
            else if (typeof(T).CheckParents(typeof(Tool), false))
            {
                sametypeEquipment = Tools.Where(c => (c.GetType().FullName == typeFullName) && (c.Index == index));
            }
            else
            {
                throw new NotImplementedException();
            }

            if (sametypeEquipment.Count() == 0)
            {
                if (index == 0)
                {
                    throw new MrKupidoException("There are no classes of type '{0}' in the equipment group. Please add it to the 'SelectEquipment' method of the recipe.", typeFullName);
                }
                else
                {
                    List<T> createdEquipment = new List<T>();
                    createdEquipment.Add(Activator.CreateInstance<T>());
                    createdEquipment[0].Index = index;

                    sametypeEquipment = createdEquipment;

                    if (typeof(T).CheckParents(typeof(Container), false))
                    {
                        Containers.Add(createdEquipment[0] as Container);
                    }
                    else if (typeof(T).CheckParents(typeof(Device), false))
                    {
                        Devices.Add(createdEquipment[0] as Device);
                    }
                    else if (typeof(T).CheckParents(typeof(Material), false))
                    {
                        Materials.Add(createdEquipment[0] as Material);
                    }
                    else if (typeof(T).CheckParents(typeof(Tool), false))
                    {
                        Tools.Add(createdEquipment[0] as Tool);
                    }
                }
            }

            result = sametypeEquipment.FirstOrDefault(c => (!c.IsDirty) || (index > 0)) as T;

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
                if (container.IsDirty) container.WashUp();
            }
            foreach (Device device in Devices)
            {
                if (device.IsDirty) device.WashUp();
            }
            foreach (Material material in Materials)
            {
                if (material.IsDirty) material.WashUp();
            }
            foreach (Tool tool in Tools)
            {
                if (tool.IsDirty) tool.WashUp();
            }
        }

        public void Merge(EquipmentGroup eg)
        {
            Containers.AddRange(eg.Containers);
            Devices.AddRange(eg.Devices);
            Materials.AddRange(eg.Materials);
            Tools.AddRange(eg.Tools);
        }
    }
}
