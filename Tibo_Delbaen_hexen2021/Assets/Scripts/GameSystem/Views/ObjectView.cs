using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public abstract class ObjectView : MonoBehaviour, IPiece
    {
        public abstract void Moved(Tile fromTile, Tile toTile);

        public abstract void Taken();
    }
}
