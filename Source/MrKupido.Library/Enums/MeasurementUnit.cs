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
		piece,

		[NameAlias("hun", "kg", Priority = 1)]
		[NameAlias("hun", "kilogramm")]
		[NameAlias("hun", "kilo", Priority = 200)]
		kilogramm,
		
		[NameAlias("hun", "dkg", Priority = 1)]
		[NameAlias("hun", "dag", Priority = 2)]
		[NameAlias("hun", "dekagramm")]
		[NameAlias("hun", "deka", Priority = 200)]
		dekagramm,
		
		[NameAlias("hun", "g", Priority = 1)]
		[NameAlias("hun", "gramm")]
		gramm,

		[NameAlias("hun", "mg", Priority = 1)]
		[NameAlias("hun", "milligramm")]
		milligramm,
		
		[NameAlias("hun", "l", Priority = 1)]
		[NameAlias("hun", "liter")]
		liter,

		[NameAlias("hun", "dl", Priority = 1)]
		[NameAlias("hun", "deciliter")]
		[NameAlias("hun", "deci", Priority = 200)]
		deciliter,

		[NameAlias("hun", "cl", Priority = 1)]
		[NameAlias("hun", "centiliter")]
		[NameAlias("hun", "cent", Priority = 200)]
		centiliter,

		[NameAlias("hun", "ml", Priority = 1)]
		[NameAlias("hun", "milliliter")]
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
		evokanal,

		[NameAlias("hun", "gyk", Priority = 1)]
		[NameAlias("hun", "gyermekkanál")]
		gyermekkanal,

		[NameAlias("hun", "tk", Priority = 1)]
		[NameAlias("hun", "teáskanál")]
		teaskanal,

		[NameAlias("hun", "kk", Priority = 1)]
		[NameAlias("hun", "kávéskanál")]
		kaveskanal,

		[NameAlias("hun", "mk", Priority = 1)]
		[NameAlias("hun", "mokkáskanál")]
		mokkaskanal,

		[NameAlias("hun", "bgr.", Priority = 1)]
		[NameAlias("hun", "bögre")]
		bogre,
		
		[NameAlias("hun", "csé.", Priority = 1)]
		[NameAlias("hun", "csésze")]
		csesze,
		
		[NameAlias("hun", "poh.", Priority = 1)]
		[NameAlias("hun", "pohár")]
		pohar,

		[NameAlias("hun", "csipet", Priority = 1)]
		[NameAlias("hun", "csipetnyi")]
		csipet,
	}
}
