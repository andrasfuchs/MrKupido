using System;
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

        protected IngredientGroup contents = new IngredientGroup();
        [NameAlias("hun", "{} tartalma")]
        public IIngredient Contents 
        {
            get
            {
                return getContents();
            }
        }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        protected IIngredient getContents()
        {
                if (contents.IngredientCount == 0)
                {
                    return null;
                }
                else if (contents.IngredientCount == 1)
                {
                    return contents.Ingredients[0];
                }
                else
                {
                    return contents;
                }
        }

        [NameAlias("eng", "wait", Priority = 200)]
        [NameAlias("hun", "vár", Priority = 200)]
        [NameAlias("hun", "várj {0} percet")]
        [PassiveAction]
        public void Varni(int minutes) 
        {
            this.LastActionDuration = (uint)(minutes * 60);
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "leönt", Priority = 200)]
        [NameAlias("hun", "öntsd le a folyadékot a(z) {L}")]
        public void FolyadekotLeonteni() 
        {
            List<IIngredient> newContent = new List<IIngredient>();

            foreach (IIngredient i in contents.Ingredients)
            {
                if (i.Unit != MeasurementUnit.liter) newContent.Add(i);
            }

            this.LastActionDuration = 120;

            this.Empty();
            this.AddRange(newContent.ToArray());
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "átönt", Priority = 200)]
        [NameAlias("hun", "öntsd át a folyadékot a(z) {L} a(z) {0B}")]
        public void FolyadekotAtonteni(IIngredientContainer c)
        {
            List<IIngredient> newContent = new List<IIngredient>();
            List<IIngredient> liquids = new List<IIngredient>();

            foreach (IIngredient i in contents.Ingredients)
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

            this.LastActionDuration = 120;

            this.Empty();
            this.AddRange(newContent.ToArray());

            c.AddRange(liquids.ToArray());
        }






        [NameAlias("eng", "add", Priority = 200)]
        [NameAlias("hun", "hozzáad", Priority = 200)]
        public void Add(IIngredient i)
        {
            this.contents.AddIngredients(i);
        }

        [NameAlias("eng", "add more", Priority = 200)]
        [NameAlias("hun", "többeket hozzáad", Priority = 200)]
        public void AddRange(IIngredient[] i)
        {
            this.contents.AddIngredients(i);
        }

        [NameAlias("eng", "empty", Priority = 200)]
        [NameAlias("hun", "kiürít", Priority = 200)]
        public void Empty()
        {
            this.contents = new IngredientGroup();
        }
    }
}
