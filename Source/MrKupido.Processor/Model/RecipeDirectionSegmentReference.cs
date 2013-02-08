﻿using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class RecipeDirectionSegmentReference : RecipeDirectionSegment
    {
        public int Id { get; private set; }
        public string IconUrl { get; set; }
        public string IconAlt { get; set; }
        public object Reference { get; private set; }
        public TreeNode TreeNode { get; set; }

        public RecipeDirectionSegmentReference(object reference, List<string> seenIngredients)
            : base("")
        {
            this.Reference = reference;

            if (reference == null)
            {
                this.Text = "(null)";
            }
            else
            {
                this.Text = reference.ToString();

                if (reference is IngredientBase)
                {
                    IngredientBase ib = (IngredientBase)reference;

                    IngredientTreeNode itn = Cache.Ingredient[ib.GetType().FullName];
                    if (itn != null)
                    {
                        this.TreeNode = itn;
                    }
                    else
                    {
                        RecipeTreeNode rtn = Cache.Recipe[ib.GetType().FullName];
                        if (rtn != null)
                        {
                            this.TreeNode = rtn;
                        }
                    }

                    if (seenIngredients.Contains(ib.ToString(false, false)))
                    {
                        this.Text = ib.ToString(false, true);
                    }
                    else
                    {
                        seenIngredients.Add(ib.ToString(false, false));
                    }
                }

                if (reference is Container)
                {
                    Container c = (Container)reference;

                    EquipmentTreeNode etn = Cache.Equipment[c.GetType().FullName];
                    if (etn != null)
                    {
                        this.TreeNode = etn;
                    }

                    this.Id = c.Id;
                    this.IconAlt = IntegerToStringHun(c.Id);
                }
                else if (reference is IngredientGroup)
                {
                    IngredientGroup ig = (IngredientGroup)reference;

                    this.Id = ig.Id;
                    this.IconAlt = IntegerToStringHun(this.Id);
                    this.Text = String.IsNullOrEmpty(ig.IconUrl) ? this.Text = ig.Name : IntegerToStringHun(this.Id);
                }
            }

            // remove commercial text { }
            int beforeStart = this.Text.IndexOf('{');
            int afterEnd = this.Text.LastIndexOf('}') + 1;

            if ((beforeStart >= 0) && (afterEnd >= 0))
            {
                this.Text = this.Text.Remove(beforeStart, afterEnd - beforeStart).TrimEnd();
            }
        }

        private string IntegerToStringHun(int i)
        {
            if ((i < 0) || (i >= 100)) throw new MrKupidoException("IntegerToStringHun supports only pozitive 1- and 2-digit integers.");

            string[] oneDigit = { "nullás", "egyes", "kettes", "hármas", "négyes", "ötös", "hatos", "hetes", "nyolcas", "kilences" };
            string[] twoDigitZero = { "", "tízes", "húszas", "hármincas", "négyvenes", "ötvenes", "hatvanas", "hetvenes", "nyolcvanas", "kilencvenes" };
            string[] twoDigitNonZero = { "", "tízen", "húszon", "hárminc", "négyven", "ötven", "hatvan", "hetven", "nyolcvan", "kilencven" };

            int lastDigit = i % 10;
            int secondLastDigit = (i / 10) % 10;

            if ((lastDigit == 0) && (secondLastDigit > 0))
            {
                return twoDigitZero[secondLastDigit];
            }
            else
            {
                return twoDigitNonZero[secondLastDigit] + oneDigit[lastDigit];
            }
        }

        public override string TextOnly()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.Text.Length; i++)
            {
                if (Char.IsNumber(this.Text[i]))
                {
                    if ((i + 1 < this.Text.Length) && Char.IsNumber(this.Text[i + 1]))
                    {
                        // two- or more-digit number
                        sb.Append(IntegerToStringHun(Int32.Parse(this.Text[i].ToString() + this.Text[i + 1].ToString())) + " ");
                        i++;
                    }
                    else
                    {
                        // one-digit number
                        sb.Append(IntegerToStringHun(Int32.Parse(this.Text[i].ToString())) + " ");
                    }
                }
                else
                {
                    sb.Append(this.Text[i]);
                }
            }

            return (String.IsNullOrEmpty(this.IconAlt) ? sb.ToString() : this.IconAlt + " " + sb.ToString());
        }
    }
}
