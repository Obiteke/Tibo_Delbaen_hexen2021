using System;

namespace DeckSystem.Utils
{
    public class CardNameAttribute : Attribute
    {
        public string Name;

        public CardNameAttribute(string name)
        {
            Name = name;
        }
    }
}
