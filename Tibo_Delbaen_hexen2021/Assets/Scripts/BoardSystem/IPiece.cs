using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{
    public interface IPiece
    {
        void Moved(Tile fromTile, Tile toTile);
        void Taken();
    }
}
