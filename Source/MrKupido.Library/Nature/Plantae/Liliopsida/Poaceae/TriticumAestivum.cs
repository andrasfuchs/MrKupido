using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "wheat")]
    [NameAlias("hun", "kenyérbúza", Priority = 1)]
    [NameAlias("hun", "közönséges búza")]

    [ContainsGluten]
    [NatureSpecies]
    public class TriticumAestivum : Poaceae
    {
    }
}
