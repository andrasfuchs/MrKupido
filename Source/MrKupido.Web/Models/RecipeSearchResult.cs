﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Processor.Model;
using System.Reflection;

namespace MrKupido.Web.Models
{
    public class RecipeSearchResult
    {
        private string orderBy;
        public string OrderBy
        {
            get
            {
                return orderBy;
            }

            set
            {
                orderBy = value;
                ReorderItemList(orderBy, isOrderDescending);
            }
        }

        private bool isOrderDescending;
        public bool IsOrderDescending
        {
            get
            {
                return isOrderDescending;
            }

            set
            {
                isOrderDescending = value;
                ReorderItemList(orderBy, isOrderDescending);
            }
        }


        public List<RecipeCategoryGroup> CategoryGroups = new List<RecipeCategoryGroup>();
        public List<RecipeSearchResultItem> Items = new List<RecipeSearchResultItem>();

        public RecipeSearchResult(RecipeTreeNode[] results)
        {
            Items = results.Select(r => new RecipeSearchResultItem(r)).ToList();

            // TODO: build category groups
        }

        private void ReorderItemList(string orderBy, bool isOrderDescending)
        {
            Items.Sort(delegate(RecipeSearchResultItem x, RecipeSearchResultItem y)
            {
                IComparable objvalue1 = GetValue(x, orderBy);
                IComparable objvalue2 = GetValue(y, orderBy); 

                if (!isOrderDescending)
                    return objvalue1.CompareTo(objvalue2);
                else
                    return objvalue2.CompareTo(objvalue1);
            });
        }

        private IComparable GetValue(object o, string propertyOrFieldName)
        {
            IComparable result = null;

            Type objType = o.GetType();
            PropertyInfo p1 = objType.GetProperty(orderBy);
            if (p1 != null)
            {
                result = (IComparable)p1.GetValue(o, null);
            }
            else
            {
                FieldInfo f1 = objType.GetField(orderBy);
                if (f1 != null)
                {
                    result = (IComparable)f1.GetValue(o);
                }
            }

            return result;
        }
        
        public RecipeSearchResultItem[] GetItems(int startIndex, int count)
        {
            List<RecipeSearchResultItem> result = new List<RecipeSearchResultItem>(Items);

            if (startIndex > 0)
            {
                result.RemoveRange(0, startIndex);
            }

            if ((count > 0) && (result.Count > count))
            {
                result.RemoveRange(count, result.Count);
            }

            return result.ToArray();
        }
    }
}