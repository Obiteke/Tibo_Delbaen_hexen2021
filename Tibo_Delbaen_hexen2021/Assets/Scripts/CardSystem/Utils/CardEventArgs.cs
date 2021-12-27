using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hexen.CardSystem.Utils
{
    public class CardEventArgs : EventArgs
    {
        public string Card { get; }

        public CardEventArgs(string card) { Card = card; }
    }
}
