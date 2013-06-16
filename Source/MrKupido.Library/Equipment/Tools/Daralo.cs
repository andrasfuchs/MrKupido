using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "crusher")]
    [NameAlias("hun", "daráló")]
    public class Daralo : Tool
    {
        [NameAlias("eng", "crush", Priority = 200)]
        [NameAlias("hun", "darál", Priority = 200)]
		[NameAlias("eng", "crush the {1} into the {0}")]
        [NameAlias("hun", "daráld le a(z) {1T} a(z) {0B}")]
        public void DaralniI(IIngredientContainer ic, ISingleIngredient i)
        {
			if (!i.IsSolid) throw new InvalidActionForIngredientException("DaralniI", i);

            if (i.Unit != MeasurementUnit.gramm) i.ChangeUnitTo(MeasurementUnit.gramm);

            i.State |= IngredientState.Daralt;

            ic.Add(i);

            this.LastActionDuration = 180;
        }
    }
}
