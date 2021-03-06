﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "wooden spoon")]
    [NameAlias("hun", "fakanál")]
    public class Fakanal : Kanal
    {
        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("eng", "mix the {0} and {1.Contents.} together")]
        [NameAlias("hun", "keverd össze a(z) {0} és {1.Contents.T}")]
        public void OsszekeverniCC(IIngredientContainer container1, IIngredientContainer container2)
        {
            container1.Add(container2.Contents);
            container2.Empty();

            this.LastActionDuration = 180;
        }

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("eng", "mix the {0.Contents.} together")]
        [NameAlias("hun", "alaposan keverd össze a(z) {0.Contents.T}")]
        public void ElkeverniC(IIngredientContainer container)
        {
            this.LastActionDuration = 180;
        }

        [NameAlias("eng", "mix up", Priority = 200)]
        [NameAlias("hun", "keverget", Priority = 200)]
        [NameAlias("eng", "mix the {0.Contents.} up for {1} minutes")]
        [NameAlias("hun", "kevergesd a {0.Contents.T} {1} percig")]
        public void KevergetniC(IIngredientContainer container, uint timeInMinutes)
        {
            this.LastActionDuration = 60 * timeInMinutes;
        }
    }
}
