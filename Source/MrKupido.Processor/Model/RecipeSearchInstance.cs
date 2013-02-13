﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class RecipeSearchInstance
    {
        public DateTime SearchStartedAt;
        public DateTime SearchFinishedAt;
        public DateTime ResultExpiresAt;
        public FilterCondition[] FilterConditions;
        public List<RecipeTreeNode> Results;
    }
}
