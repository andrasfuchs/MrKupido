using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "chicken")]
    [NameAlias("hun", "csirke", Priority = 1)]
    [NameAlias("hun", "házityúk")]

    [NatureSubspecies]
    public class GallusGallusDomesticus : GallusGallus
    {
    }
}
