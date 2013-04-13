using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{

    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class UnitConstsAttribute : Attribute
    {
		public float SmallestAmount = 0.01f;

		public float BiggestAmount = Single.MaxValue;

		public MeasurementUnit UnitDown = MeasurementUnit.none;

		public MeasurementUnit UnitUp = MeasurementUnit.none;
    }
}
