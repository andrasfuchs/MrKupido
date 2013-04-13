using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public enum MeasurementUnit {
		[NameAlias("hun", "")]
		none,

		[NameAlias("hun", "db", Priority = 1)]
		[NameAlias("hun", "darab")]
		[UnitConsts(SmallestAmount = 1.0f)]
		piece,

		[NameAlias("hun", "kg", Priority = 1)]
		[NameAlias("hun", "kilogramm")]
		[NameAlias("hun", "kilo", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.25f, UnitDown = MeasurementUnit.dekagramm)]
		kilogramm,
		
		[NameAlias("hun", "dkg", Priority = 1)]
		[NameAlias("hun", "dag", Priority = 2)]
		[NameAlias("hun", "dekagramm")]
		[NameAlias("hun", "deka", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 100.0f, UnitDown = MeasurementUnit.gramm, UnitUp = MeasurementUnit.kilogramm)]
		dekagramm,
		
		[NameAlias("hun", "g", Priority = 1)]
		[NameAlias("hun", "gramm")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.milligramm, UnitUp = MeasurementUnit.dekagramm)]
		gramm,

		[NameAlias("hun", "mg", Priority = 1)]
		[NameAlias("hun", "milligramm")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 1000.0f, UnitUp = MeasurementUnit.gramm)]
		milligramm,
		
		[NameAlias("hun", "l", Priority = 1)]
		[NameAlias("hun", "liter")]
		[UnitConsts(SmallestAmount = 0.25f, UnitDown = MeasurementUnit.deciliter)]
		liter,

		[NameAlias("hun", "dl", Priority = 1)]
		[NameAlias("hun", "deciliter")]
		[NameAlias("hun", "deci", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		deciliter,

		[NameAlias("hun", "cl", Priority = 1)]
		[NameAlias("hun", "centiliter")]
		[NameAlias("hun", "cent", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.milliliter, UnitUp = MeasurementUnit.deciliter)]
		centiliter,

		[NameAlias("hun", "ml", Priority = 1)]
		[NameAlias("hun", "milliliter")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 50.0f, UnitUp = MeasurementUnit.centiliter)]
		milliliter,

		[NameAlias("hun", "m", Priority = 1)]
		[NameAlias("hun", "méter")]
		meter,

		[NameAlias("hun", "dm", Priority = 1)]
		[NameAlias("hun", "deciméter")]
		decimeter,

		[NameAlias("hun", "cm", Priority = 1)]
		[NameAlias("hun", "centiméter")]
		centimeter,

		[NameAlias("hun", "mm", Priority = 1)]
		[NameAlias("hun", "milliméter")]
		millimeter,

		[NameAlias("hun", "fm.", Priority = 1)]
		[NameAlias("hun", "folyóméter")]
		folyometer,

		[NameAlias("hun", "IU")]
		IU,

		[NameAlias("hun", "Celsius")]
		Celsius,

		[NameAlias("hun", "másodperc")]
		second,

		[NameAlias("hun", "kcal", Priority = 1)]
		[NameAlias("hun", "kalória")]
		calories,

		[NameAlias("hun", "adag")]
		portion,

		[NameAlias("hun", "ek", Priority = 1)]
		[NameAlias("hun", "evőkanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.kaveskanal, UnitUp = MeasurementUnit.deciliter)]
		evokanal,

		[NameAlias("hun", "gyk", Priority = 1)]
		[NameAlias("hun", "gyermekkanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.kaveskanal, UnitUp = MeasurementUnit.evokanal)]
		gyermekkanal,

		[NameAlias("hun", "tk", Priority = 1)]
		[NameAlias("hun", "teáskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.mokkaskanal, UnitUp = MeasurementUnit.evokanal)]
		teaskanal,

		[NameAlias("hun", "kk", Priority = 1)]
		[NameAlias("hun", "kávéskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.mokkaskanal, UnitUp = MeasurementUnit.evokanal)]
		kaveskanal,

		[NameAlias("hun", "mk", Priority = 1)]
		[NameAlias("hun", "mokkáskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.milliliter, UnitUp = MeasurementUnit.kaveskanal)]
		mokkaskanal,

		[NameAlias("hun", "bgr.", Priority = 1)]
		[NameAlias("hun", "bögre")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		bogre,
		
		[NameAlias("hun", "csé.", Priority = 1)]
		[NameAlias("hun", "csésze")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		csesze,

		[NameAlias("hun", "poh.", Priority = 1)]
		[NameAlias("hun", "pohár")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		pohar,

		[NameAlias("hun", "csipet", Priority = 1)]
		[NameAlias("hun", "csipetnyi")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 5.0f, UnitUp = MeasurementUnit.dekagramm)]
		csipet,
	}
}
