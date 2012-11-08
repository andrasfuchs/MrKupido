using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "tároló")]
    [NameAlias("eng", "container")]

    public abstract class Container : EquipmentBase, IIngredientContainer
    {
        public Dimensions Dimensions { get; protected set; }

        public IIngredient Contents { get; set; }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        [NameAlias("hun", "rakd be a(z) {B} a {0T}")]
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

        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            IIngredient i = this.Contents;
            this.Contents = null;

            this.LastActionDuration = 60;

            return i;
        }

        [NameAlias("hun", "várj {0} percet")]
        public void Varni(int minutes) 
        {
            this.LastActionDuration = (uint)(minutes * 60);
        }

        [NameAlias("hun", "fedd le a(z) {T} {0V}")]
        public void Lefedni(Material material) 
        {
            this.LastActionDuration = 60;
        }

        [NameAlias("hun", "vedd le a fedőt")]
        public void FedotLevenni() 
        {
            this.LastActionDuration = 60;
        }

        [NameAlias("hun", "öntsd le a folyadékot a(z) {L}")]
        public IngredientGroup FolyadekotLeonteni() 
        {
            List<IIngredient> result = new List<IIngredient>();

            if (Contents is IngredientGroup)
            {
                foreach (IIngredient i in ((IngredientGroup)Contents).Ingredients)
                {
                    if (i.Unit != MeasurementUnit.liter) result.Add(i);
                }
            } 
            else 
            {
                throw new InvalidActionForIngredientException("FolyadekotLeonteni", Contents.Name, Contents.Unit);
            }

            this.LastActionDuration = 120;

            return new IngredientGroup(result.ToArray());
        }
    }
}
