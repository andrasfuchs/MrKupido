using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "oregano")]
    [NameAlias("hun", "oregánó", Priority = 1)]
    [NameAlias("hun", "szurokfű")]
    [NameAlias("hun", "közönséges szurokfű", Priority = 200)]
    [NameAlias("hun", "vadmajoránna", Priority = 201)]

    [NatureSpecies]
    public class OriganumVulgareL : Lamiaceae
    {
    }
}