using Hexen.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
