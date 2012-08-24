using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "cukor", Priority = 1)]
    [NameAlias("hun", "kristálycukor", Priority = 2)]
    [NameAlias("hun", "répacukor")]
    [NameAlias("hun", "asztali cukor", Priority = 200)]
    [NameAlias("hun", "étkezési cukor", Priority = 201)]
    [NameAlias("hun", "finomított cukor", Priority = 202)]
    [NameAlias("eng", "cheese")]

    public class Cukor : RecipeBase
    {
        public Cukor(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}