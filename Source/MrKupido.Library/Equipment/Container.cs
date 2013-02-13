﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "container")]
    [NameAlias("hun", "tároló")]
    public abstract class Container : EquipmentBase, IIngredientContainer
    {
        public Dimensions Dimensions { get; protected set; }

        public int Id { get; set; }
        
        public IIngredient Contents { get; set; }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a(z) {0T}")]
        public bool Berakni(IIngredient ig)
        {
            // TODO: return if it fits in
            if (this.Contents != null)
            {
                this.Contents = new IngredientGroup(new IIngredient[] { this.Contents, ig });
            }
            else
            {
                this.Contents = ig;
            }

            this.LastActionDuration = 60;

            return true;
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a következőket: ({0*}, )")]
        public bool BerakniMind(params IIngredient[] ingredients)
        {
            IngredientGroup contents = null;

            if (this.Contents != null)
            {
                List<IIngredient> ings = new List<IIngredient>(ingredients);
                ings.Add(this.Contents);
                contents = new IngredientGroup(ings.ToArray());
            }
            else
            {
                contents = new IngredientGroup(ingredients);
            }

            this.Contents = contents;
            this.LastActionDuration = 60 * (uint)ingredients.Length;

            return true;
        }


        [NameAlias("eng", "pour out", Priority = 200)]
        [NameAlias("hun", "beönt", Priority = 200)]
        [NameAlias("hun", "öntsd be a(z) {B} a {0T}")]
        public bool Beonteni(IIngredient ig)
        {
            // TODO: return if it fits in
            if (this.Contents != null)
            {
                this.Contents = new IngredientGroup(new IIngredient[] { this.Contents, ig });
            }
            else
            {
                this.Contents = ig;
            }

            this.LastActionDuration = 60;

            return true;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kivesz", Priority = 200)]
        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            IIngredient i = this.Contents;
            this.Contents = null;

            this.LastActionDuration = 60;

            if ((i is IngredientGroup) && ((IngredientGroup)i).Ingredients.Length == 1)
            {
                return ((IngredientGroup)i).Ingredients[0];
            }

            return i;
        }

        [NameAlias("eng", "wait", Priority = 200)]
        [NameAlias("hun", "vár", Priority = 200)]
        [NameAlias("hun", "várj {0} percet")]
        [PassiveAction]
        public void Varni(int minutes) 
        {
            this.LastActionDuration = (uint)(minutes * 60);
        }

        [NameAlias("eng", "cover", Priority = 200)]
        [NameAlias("hun", "lefed", Priority = 200)]
        [NameAlias("hun", "fedd le a(z) {T} {0V}")]
        public void Lefedni(Material material) 
        {
            this.LastActionDuration = 5;
        }

        [NameAlias("eng", "take off", Priority = 200)]
        [NameAlias("hun", "levesz", Priority = 200)]
        [NameAlias("hun", "vedd le a fedőt")]
        public void FedotLevenni() 
        {
            this.LastActionDuration = 5;
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "leönt", Priority = 200)]
        [NameAlias("hun", "öntsd le a folyadékot a(z) {L}")]
        public void FolyadekotLeonteni() 
        {
            List<IIngredient> newContent = new List<IIngredient>();

            if (Contents is IngredientGroup)
            {
                foreach (IIngredient i in ((IngredientGroup)Contents).Ingredients)
                {
                    if (i.Unit != MeasurementUnit.liter) newContent.Add(i);
                }
            } 
            else 
            {
                if (Contents.Unit != MeasurementUnit.liter)
                {
                    throw new InvalidActionForIngredientException("FolyadekotLeonteni", Contents.Name, Contents.Unit);
                }
            }

            this.LastActionDuration = 120;

            this.Contents = new IngredientGroup(newContent.ToArray());
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "átönt", Priority = 200)]
        [NameAlias("hun", "öntsd át a folyadékot a(z) {L} a(z) {0B}")]
        public void FolyadekotAtonteni(Container c)
        {
            List<IIngredient> newContent = new List<IIngredient>();
            List<IIngredient> liquids = new List<IIngredient>();

            if (Contents is IngredientGroup)
            {
                foreach (IIngredient i in ((IngredientGroup)Contents).Ingredients)
                {
                    if (i.Unit != MeasurementUnit.liter)
                    {
                        newContent.Add(i);
                    }
                    else 
                    {
                        liquids.Add(i);
                    }
                }
            }
            else
            {
                if (Contents.Unit != MeasurementUnit.liter)
                {
                    throw new InvalidActionForIngredientException("FolyadekotLeonteni", Contents.Name, Contents.Unit);
                }
            }

            this.LastActionDuration = 120;

            this.Contents = new IngredientGroup(newContent.ToArray());

            liquids.Add(c.Contents);
            c.Contents = new IngredientGroup(liquids.ToArray());
        }
    
    }
}
