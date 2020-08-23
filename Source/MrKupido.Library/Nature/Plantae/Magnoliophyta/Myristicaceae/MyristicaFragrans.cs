using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "nutmeg")]
    [NameAlias("hun", "szerecsendiófa")]
    [NameAlias("hun", "muskátdió", Priority = 200)]

    [NatureSpecies]
    public class MyristicaFragrans : Myristicaceae
    {
    }
}
