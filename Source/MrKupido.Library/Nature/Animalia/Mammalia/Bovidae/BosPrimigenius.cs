using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "cow")]
    [NameAlias("hun", "tehén", Priority = 1)]
    [NameAlias("hun", "szarvasmarha")]

    [NatureSpecies]
    public class BosPrimigenius : Bovidae
    {
    }
}
