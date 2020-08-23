using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "deep container with cover")]
    [NameAlias("hun", "fedős mély tároló")]
    public class CoverableContainer : DeepContainer
    {
        private Material fedo = null;

        public CoverableContainer(float width, float height, float depth)
            : base(width, height, depth)
        {
        }

        [NameAlias("eng", "cover", Priority = 200)]
        [NameAlias("hun", "lefed", Priority = 200)]
        [NameAlias("eng", "cover the {} with {0}")]
        [NameAlias("hun", "fedd le a(z) {T} {0V}")]
        public void Lefedni(Material material)
        {
            this.fedo = material;

            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "take off", Priority = 200)]
        [NameAlias("hun", "levesz", Priority = 200)]
        [NameAlias("eng", "take off the {-} from the {}")]
        [NameAlias("hun", "vedd le a(z) {L} a(z) {-T}")]
        public Material FedotLevenni()
        {
            this.LastActionDuration = 60;

            return this.fedo;
        }
    }
}
