using System;
using System.Collections;
using System.Collections.Generic;
using DeckSystem.Utils;
using UnityEngine;

namespace DeckSystem
{
    public class Hand<TCard>
    {
        private readonly Deck<TCard> _deck;
        private readonly int _maxCardCount;
        public List<string> Cards { get; } = new List<string>();
        internal Hand(Deck<TCard> deck, int maxCards)
        {
            _deck = deck;
            _maxCardCount = maxCards;
            FillHand();
        }

        public void FillHand()
        {
            for (int i = Cards.Count; i < _maxCardCount; i++)
            {
                if (_deck.DrawCard(out string card))
                    AddCard(card);
            }
        }
        public void AddCard(string card, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Cards.Add(card);
                OnCardAdded(new CardEventArgs(card));
            }
        }
        public void RemoveCard(string card)
        {
            Cards.Remove(card);
            OnCardRemoved(new CardEventArgs(card));
        }

        protected virtual void OnCardAdded(CardEventArgs args)
        {
            EventHandler<CardEventArgs> eventHandler = CardAdded;
            if (eventHandler == null)
                return;

            eventHandler(this, args);
        }
        protected virtual void OnCardRemoved(CardEventArgs args)
        {
            EventHandler<CardEventArgs> eventHandler = CardRemoved;
            if (eventHandler == null)
                return;

            eventHandler(this, args);
        }

        public event EventHandler<CardEventArgs> CardAdded;
        public event EventHandler<CardEventArgs> CardRemoved;

    }
}
