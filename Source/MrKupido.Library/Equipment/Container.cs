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

        private IngredientGroup contents = new IngredientGroup();
        public IIngredient Contents 
        {
            get
            {
                if (contents.Count == 0)
                {
                    return null;
                }
                else if (contents.Count == 1)
                {
                    return contents.Ingredients[0];
                }
                else
                {
                    return contents;
                }
            }
        }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a következőket: ({0*}, )")]
        public void Berakni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 30 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a {0} tartalmát")]
        public void BerakniEdenybol(IIngredientContainer ic)
        {
            this.contents.AddIngredients(ic.Contents);
            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "pour out", Priority = 200)]
        [NameAlias("hun", "beönt", Priority = 200)]
        [NameAlias("hun", "öntsd be a(z) {B} a következőket: ({0*}, )")]
        public void Beonteni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 15 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kivesz", Priority = 200)]
        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            IIngredient i = this.Contents;
            this.Empty();

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

        public void Add(IIngredient i)
        {
            this.contents.AddIngredients(i);
        }

        public void AddRange(IIngredient[] i)
        {
            this.contents.AddIngredients(i);
        }

        public void Empty()
        {
            this.contents = new IngredientGroup();
        }
    }
}
