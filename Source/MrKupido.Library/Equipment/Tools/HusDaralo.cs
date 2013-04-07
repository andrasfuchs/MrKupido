using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
	[NameAlias("eng", "mincing machine")]
    [NameAlias("hun", "húsdaráló")]
    public class HusDaralo : Tool
    {
        [NameAlias("eng", "crush", Priority = 200)]
        [NameAlias("hun", "darál", Priority = 200)]
        [NameAlias("hun", "daráld le a(z) {1T} a(z) {0B}")]
        public void DaralniC(IIngredientContainer icInto, IIngredientContainer ic)
        {
			IIngredient i = ic.Contents;

			if (!i.IsSolid) throw new InvalidActionForIngredientException("DaralniC", i);

            if (i.Unit != MeasurementUnit.gramm) i.ChangeUnitTo(MeasurementUnit.gramm);

            i.State |= IngredientState.Daralt;

            icInto.Add(ic.Contents);
			ic.Empty();

            this.LastActionDuration = 240;
        }
    }
}
