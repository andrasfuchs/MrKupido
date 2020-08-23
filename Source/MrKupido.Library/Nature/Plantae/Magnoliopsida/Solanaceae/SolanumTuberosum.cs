using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "potato")]
    [NameAlias("hun", "krumpli", Priority = 1)]
    [NameAlias("hun", "burgonya")]
    [NameAlias("hun", "kolompér", Priority = 200)]
    [NameAlias("hun", "krompé", Priority = 201)]
    [NameAlias("hun", "pityóka", Priority = 202)]

    [NatureSpecies]
    public class SolanumTuberosum : Solanaceae
    {
    }
}
