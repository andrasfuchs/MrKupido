﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class EquipmentCache : BaseCache
    {
        public new EquipmentTreeNode this[string name]
        {
            get
            {
                return (EquipmentTreeNode)base[name];
            }
        }

        public void Initialize()
        {
            EquipmentTreeNode root = TreeNode.BuildTree(Cache.Assemblies, t => new EquipmentTreeNode(t), typeof(MrKupido.Library.Equipment.EquipmentBase));
            Indexer = new Indexer(root);

            WasInitialized = true;
        }
    }
}