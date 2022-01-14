using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece> where TPiece : class, IPiece
    {
        bool CanExecute(Board<TPiece> board, TPiece piece);

        List<Tile> Tiles(Board<TPiece> board, TPiece piece);

        void Execute(Board<TPiece> board, TPiece piece, Tile toTile);
    }
}
    