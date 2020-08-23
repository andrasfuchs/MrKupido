using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "spoon")]
    [NameAlias("hun", "kanál")]
    public class Kanal : Tool
    {
        [NameAlias("eng", "skim", Priority = 200)]
        [NameAlias("hun", "lefölöz", Priority = 200)]
        [NameAlias("eng", "skim the contents of {}")]
        [NameAlias("hun", "fölözd le a(z) {} tartalmát")]
        public IIngredient LefolozniC(IIngredientContainer container, float percent)
        {
            IIngredient i = container.Contents;

            // TODO: return x% of its contents and leave the rest in the container

            this.LastActionDuration = 60;

            return i;
        }

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("eng", "mix the {0.Contents.} together")]
        [NameAlias("hun", "alaposan keverd össze a(z) {0.Contents.T}")]
        public void ElkeverniC(IIngredientContainer container)
        {
            this.LastActionDuration = 180;
        }
    }
}
