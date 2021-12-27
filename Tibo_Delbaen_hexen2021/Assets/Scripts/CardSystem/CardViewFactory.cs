using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexen.CardSystem
{
    [CreateAssetMenu(menuName = "DAE/CardView Factory")]
    public class CardViewFactory : ScriptableObject
    {
        [SerializeField]
        private List<CardView> _cardViews = new List<CardView>();

        [SerializeField]
        private List<string> _cardNames = new List<string>();

        private Canvas _canvas;
        public Canvas Canvas
        {
            get
            {
                if(_canvas == null)
                    _canvas = GameObject.Find("UI").GetComponent<Canvas>();

                return _canvas;
            }
        }

        public CardView CreateView(string card, Transform transform)
        {
            int num = _cardNames.IndexOf(card);
            if (num == -1)
                return null;

            var cardView = Instantiate(_cardViews.ElementAt(num), transform);
            //Debug.Log(_cardViews.ElementAt(num) + " + " + transform);
            //cardView.Model = card;
            cardView.DragArea = (RectTransform)Canvas.transform;
            cardView.name = $"Card ({card})";
            return cardView;
        }
    }
}

