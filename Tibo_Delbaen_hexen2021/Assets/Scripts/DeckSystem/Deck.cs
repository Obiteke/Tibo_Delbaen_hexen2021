using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DeckSystem
{
    public class Deck<TCard>
    {
        private readonly Random _random = new Random();
        public List<string> Cards { get; } = new List<string>();

        private readonly Dictionary<string, TCard> _cardActions = new Dictionary<string, TCard>();
        public Deck()
        {

        }
        public Hand<TCard> DealHand(int maxCards)
        {
            return new Hand<TCard>(this, maxCards);
        }
        public void Shuffle(int _amount)
        {
            AddCard("Charge", _amount);
            AddCard("Push", _amount);
            AddCard("Swipe", _amount);
            AddCard("Teleport", _amount);
        }
        public void AddCard(string card, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Cards.Add(card);
            }
        }
        public bool DrawCard(out string card)
        {
            card = null;
            if (Cards.Count == 0)
                return false;

            int num = _random.Next(Cards.Count);
            card = Cards.ElementAt(num);
            Cards.RemoveAt(num);
            return true;
        }
    }
}
