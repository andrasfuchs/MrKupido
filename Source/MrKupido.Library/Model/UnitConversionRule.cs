using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
	public class UnitConversionRule
	{
		public MeasurementUnit From { get; private set; }
		public MeasurementUnit To { get; private set; }
		public string Command { get; private set; }
		public string Parameter { get; private set; }
		public bool Reversable { get; private set; }

		public UnitConversionRule(MeasurementUnit from, MeasurementUnit to, string command, string parameter, bool reversable)
		{
			this.From = from;
			this.To = to;
			this.Command = command;
			this.Parameter = parameter;
			this.Reversable = reversable;
		}

		public override string ToString()
		{
			return this.From + " -> " + this.To;
		}
	}
}
