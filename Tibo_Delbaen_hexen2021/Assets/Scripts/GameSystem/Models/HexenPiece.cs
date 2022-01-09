using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexen.BoardSystem;

namespace Hexen.GameSystem.Models
{
    public class HexenPiece : PieceBase
    {
        public Tile Target { get; set; }
        public List<Tile> Path { get; set; }

        public HexenPiece()
        {

        }
    }
}
