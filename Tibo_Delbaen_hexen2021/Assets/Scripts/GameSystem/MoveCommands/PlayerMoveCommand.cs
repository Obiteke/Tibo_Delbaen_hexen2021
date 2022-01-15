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
    public class PlayerMoveCommand : AbstractBasicMoveCommand
    {
        public PlayerMoveCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        public override List<Tile> Tiles(Board<HexenPiece> board, HexenPiece piece)
        {
            Random rnd = new Random();
            return new List<Tile> { board.Tiles[16] /*board.Tiles[rnd.Next(0,board.Tiles.Count)]*/ /* board.TileOf(piece)*/ };

            //var validTiles = new MovementHelper(board, piece)
            //    .North(1, MovementHelper.IsEmpty)
            //    //.North(2, MovementHelper.IsEmpty, (b, c, t) => !piece.HasMoved)
            //    .NorthEast(1, MovementHelper.CanCapture)
            //    .NorthWest(1, MovementHelper.CanCapture)
            //    .GenerateTiles();

            //return validTiles;
        }
    }
}
