using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommands
{
    public abstract class AbstractBasicMoveCommand : IMoveCommand<HexenPiece>
    {
        protected ReplayManager ReplayManager;

        protected AbstractBasicMoveCommand(ReplayManager replayManager)
        {
            ReplayManager = replayManager;
        }

        public virtual bool CanExecute(Board<HexenPiece> board, HexenPiece piece)
        {
            var validTiles = Tiles(board, piece);
            return validTiles.Count > 0;
        }

        public virtual void Execute(Board<HexenPiece> board, HexenPiece piece, Tile toTile)
        {
            var toPiece = board.PieceAt(toTile);
            var fromTile = board.TileOf(piece);
            //var hasMoved = piece.HasMoved;

            Action forward = () =>
            {
                if (toPiece != null)
                    board.Take(toTile);

                board.Move(fromTile, toTile);

                //piece.HasMoved = true;
            };

            Action backward = () =>
            {
                //piece.HasMoved = hasMoved;

                board.Move(toTile, fromTile);

                if (toPiece != null)
                    board.Place(toTile, toPiece);
            };

            var replayComand = new DelegateReplayCommand(forward, backward);

            ReplayManager.Execute(replayComand);
        }

        public abstract List<Tile> Tiles(Board<HexenPiece> board, HexenPiece piece);
    }
}
