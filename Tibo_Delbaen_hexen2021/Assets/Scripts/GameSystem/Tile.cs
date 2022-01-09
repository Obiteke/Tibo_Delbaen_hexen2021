
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Hexen.BoardSystem;

namespace Hexen.GameSystem
{
    public class TileEventArgs : EventArgs
    {
        public Tile Tile { get; }

        public TileEventArgs(Tile tile)
        {
            Tile = tile;
        }
    }

    public class Tile : MonoBehaviour, IPointerClickHandler//, IPosition
    {

        public event EventHandler<TileEventArgs> Clicked;

        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;

        public bool Highlight
        {
            set
            {
                if (value)
                    OnActivate.Invoke();
                else
                    OnDeactivate.Invoke();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //FindObjectOfType<GameLoop>().Select(this);

            //OnClick(new TileEventArgs(this));

            //GameLoop.Instance.Select(this);
        }

        protected virtual void OnClick(TileEventArgs eventArgs)
        {
            var handler = Clicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}