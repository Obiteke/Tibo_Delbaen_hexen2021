using DeckSystem;
using DeckSystem.Utils;
using GameSystem.Models.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
    
namespace GameSystem.Views
{
    public class HandView : MonoBehaviour
    {
        [SerializeField]
        CardViewFactory _cardViewFactory;

        public bool isPlayer2Hand = false;

        private Hand<CardBase> _model;

        private readonly List<string> _cards = new List<string>();

        private readonly List<CardView> _cardViews = new List<CardView>();

        public void InitCardView()
        {
            foreach (string card in _model.Cards)
            {
                InitCardView(card);
            }
        }

        private void InitCardView(string card)
        {
            CardView cardView = _cardViewFactory.CreateView(card, transform);
            cardView.transform.SetParent(transform);
            cardView.name = $"Card ({card})";
            _cards.Add(card);
            _cardViews.Add(cardView);
        }
        private void OnCardAdded(object sender, CardEventArgs e)
        {
            InitCardView(e.Card);
        }
        private void OnCardRemoved(object sender, CardEventArgs e)
        {
            int num = _cards.IndexOf(e.Card);
            CardView card = _cardViews[num];
            _cards.RemoveAt(num);
            _cardViews.RemoveAt(num);
            card.Played();
        }
        private void OnGameLoopInitialized(object sender, EventArgs e)
        {
            if(!isPlayer2Hand)
                _model = SingletonMonoBehaviour<GameLoop>.Instance.HandPlayer1;
            else
                _model = SingletonMonoBehaviour<GameLoop>.Instance.HandPlayer2;

            _model.CardAdded += new EventHandler<CardEventArgs>(OnCardAdded);
            _model.CardRemoved += new EventHandler<CardEventArgs>(OnCardRemoved);
            InitCardView();
        }

        private void Start()
        {
            SingletonMonoBehaviour<GameLoop>.Instance.Initialized += new EventHandler(OnGameLoopInitialized);
        }
    }
}