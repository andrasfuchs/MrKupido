using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public enum MeasurementUnit {
		[NameAlias("eng", "")]
		[NameAlias("hun", "")]
		none,

		[NameAlias("eng", "piece")]
		[NameAlias("hun", "db", Priority = 1)]
		[NameAlias("hun", "darab")]
		[UnitConsts(SmallestAmount = 1.0f)]
		piece,

		[NameAlias("eng", "kg", Priority = 1)]
		[NameAlias("eng", "kilogramm")]
		[NameAlias("hun", "kg", Priority = 1)]
		[NameAlias("hun", "kilogramm")]
		[NameAlias("hun", "kilo", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.25f, UnitDown = MeasurementUnit.dekagramm)]
		kilogramm,

		[NameAlias("eng", "dkg", Priority = 1)]
		[NameAlias("eng", "dekagramm")]
		[NameAlias("hun", "dkg", Priority = 1)]
		[NameAlias("hun", "dag", Priority = 2)]
		[NameAlias("hun", "dekagramm")]
		[NameAlias("hun", "deka", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 100.0f, UnitDown = MeasurementUnit.gramm, UnitUp = MeasurementUnit.kilogramm)]
		dekagramm,

		[NameAlias("eng", "g", Priority = 1)]
		[NameAlias("eng", "gramm")]
		[NameAlias("hun", "g", Priority = 1)]
		[NameAlias("hun", "gramm")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.milligramm, UnitUp = MeasurementUnit.dekagramm)]
		gramm,

		[NameAlias("eng", "mg", Priority = 1)]
		[NameAlias("eng", "milligramm")]
		[NameAlias("hun", "mg", Priority = 1)]
		[NameAlias("hun", "milligramm")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 1000.0f, UnitUp = MeasurementUnit.gramm)]
		milligramm,

		[NameAlias("eng", "l", Priority = 1)]
		[NameAlias("eng", "liter")]
		[NameAlias("hun", "l", Priority = 1)]
		[NameAlias("hun", "liter")]
		[UnitConsts(SmallestAmount = 0.25f, UnitDown = MeasurementUnit.deciliter)]
		liter,

		[NameAlias("eng", "dl", Priority = 1)]
		[NameAlias("eng", "deciliter")]
		[NameAlias("hun", "dl", Priority = 1)]
		[NameAlias("hun", "deciliter")]
		[NameAlias("hun", "deci", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		deciliter,

		[NameAlias("eng", "cl", Priority = 1)]
		[NameAlias("eng", "centiliter")]
		[NameAlias("hun", "cl", Priority = 1)]
		[NameAlias("hun", "centiliter")]
		[NameAlias("hun", "cent", Priority = 200)]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 50.0f, UnitDown = MeasurementUnit.milliliter, UnitUp = MeasurementUnit.deciliter)]
		centiliter,

		[NameAlias("eng", "ml", Priority = 1)]
		[NameAlias("eng", "milliliter")]
		[NameAlias("hun", "ml", Priority = 1)]
		[NameAlias("hun", "milliliter")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 50.0f, UnitUp = MeasurementUnit.centiliter)]
		milliliter,

		[NameAlias("eng", "m", Priority = 1)]
		[NameAlias("eng", "meter")]
		[NameAlias("hun", "m", Priority = 1)]
		[NameAlias("hun", "méter")]
		meter,

		[NameAlias("eng", "dm", Priority = 1)]
		[NameAlias("eng", "deciméter")]
		[NameAlias("hun", "dm", Priority = 1)]
		[NameAlias("hun", "deciméter")]
		decimeter,

		[NameAlias("eng", "cm", Priority = 1)]
		[NameAlias("eng", "centiméter")]
		[NameAlias("hun", "cm", Priority = 1)]
		[NameAlias("hun", "centiméter")]
		centimeter,

		[NameAlias("eng", "mm", Priority = 1)]
		[NameAlias("eng", "milliméter")]
		[NameAlias("hun", "mm", Priority = 1)]
		[NameAlias("hun", "milliméter")]
		millimeter,

		[NameAlias("eng", "lm", Priority = 1)]
		[NameAlias("eng", "linear meter")]
		[NameAlias("hun", "fm", Priority = 1)]
		[NameAlias("hun", "folyóméter")]
		folyometer,

		[NameAlias("eng", "IU")]
		[NameAlias("hun", "IU")]
		IU,

		[NameAlias("eng", "Celsius")]
		[NameAlias("hun", "Celsius")]
		Celsius,

		[NameAlias("eng", "week")]
		[NameAlias("hun", "hét")]
		week,

		[NameAlias("eng", "day")]
		[NameAlias("hun", "nap")]
		day,

		[NameAlias("eng", "hour")]
		[NameAlias("hun", "óra")]
		hour,

		[NameAlias("eng", "minute")]
		[NameAlias("hun", "perc")]
		minute,
		
		[NameAlias("eng", "second")]
		[NameAlias("hun", "másodperc")]
		second,

		[NameAlias("eng", "kcal", Priority = 1)]
		[NameAlias("eng", "calory")]
		[NameAlias("hun", "kcal", Priority = 1)]
		[NameAlias("hun", "kalória")]
		calories,

		[NameAlias("eng", "portion")]
		[NameAlias("hun", "adag")]
		portion,

		[NameAlias("eng", "tablespoon")]
		[NameAlias("hun", "ek", Priority = 1)]
		[NameAlias("hun", "evőkanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.kaveskanal, UnitUp = MeasurementUnit.deciliter)]
		evokanal,

		[NameAlias("eng", "dessert spoon")]
		[NameAlias("hun", "gyk", Priority = 1)]
		[NameAlias("hun", "gyermekkanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.kaveskanal, UnitUp = MeasurementUnit.evokanal)]
		gyermekkanal,

		[NameAlias("eng", "teaspoon")]
		[NameAlias("hun", "tk", Priority = 1)]
		[NameAlias("hun", "teáskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.mokkaskanal, UnitUp = MeasurementUnit.evokanal)]
		teaskanal,

		[NameAlias("eng", "coffee spoon")]
		[NameAlias("hun", "kk", Priority = 1)]
		[NameAlias("hun", "kávéskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.mokkaskanal, UnitUp = MeasurementUnit.evokanal)]
		kaveskanal,

		[NameAlias("eng", "mocha spoon")]
		[NameAlias("hun", "mk", Priority = 1)]
		[NameAlias("hun", "mokkáskanál")]
		[UnitConsts(SmallestAmount = 0.5f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.milliliter, UnitUp = MeasurementUnit.kaveskanal)]
		mokkaskanal,

		[NameAlias("eng", "mug")]
		[NameAlias("hun", "bgr.", Priority = 1)]
		[NameAlias("hun", "bögre")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		bogre,

		[NameAlias("eng", "cup")]
		[NameAlias("hun", "csé.", Priority = 1)]
		[NameAlias("hun", "csésze")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		csesze,

		[NameAlias("eng", "glass")]
		[NameAlias("hun", "poh.", Priority = 1)]
		[NameAlias("hun", "pohár")]
		[UnitConsts(SmallestAmount = 0.25f, BiggestAmount = 5.0f, UnitDown = MeasurementUnit.centiliter, UnitUp = MeasurementUnit.liter)]
		pohar,

		[NameAlias("eng", "pinch")]
		[NameAlias("hun", "csipet", Priority = 1)]
		[NameAlias("hun", "csipetnyi")]
		[UnitConsts(SmallestAmount = 1.0f, BiggestAmount = 5.0f, UnitUp = MeasurementUnit.dekagramm)]
		csipet,
	}
}
