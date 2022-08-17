using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HighlightStatusChanged;
        public event EventHandler DestroyedStatusChanged;

        bool _isHighlighted = false;
        bool _isDestroyed = false;

        public Position Position { get; }
        public bool IsHighlighted
        {
            get => _isHighlighted;
            internal set
            {
                _isHighlighted = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }
        public bool IsDestroyed
        {
            get => _isDestroyed;
            internal set
            {
                _isDestroyed = value;
                OnDestroyedStatusChanged(EventArgs.Empty);
            }
        }

        public Tile(int x, int y, int z)
        {
            Position = new Position
            {
                X = x,
                Y = y,
                Z = z
            };
        }


        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);
        }
        protected virtual void OnDestroyedStatusChanged(EventArgs args)
        {
            EventHandler handler = DestroyedStatusChanged;
            handler?.Invoke(this, args);
        }
    }
}
