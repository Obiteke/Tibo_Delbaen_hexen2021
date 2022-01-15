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
    public class PlayerSwipeCommand : AbstractBasicMoveCommand
    {
        public PlayerSwipeCommand(ReplayManager replayManager) : base(replayManager)
        {
        }

        /*
         * https://www.redblobgames.com/grids/hexagons/#neighbors-cube
         * 
         *  var cube_directions = [
         *      Cube(+1, -1, 0), Cube(+1, 0, -1), Cube(0, +1, -1), 
         *      Cube(-1, +1, 0), Cube(-1, 0, +1), Cube(0, -1, +1), 
         *   ]
         *
         *  function cube_direction(direction):
         *      return cube_directions[direction]
         *
         *  function cube_neighbor(cube, direction):
         *      return cube_add(cube, cube_direction(direction)) 
         *
        */
        public override List<Tile> Tiles(Board<HexenPiece> board, HexenPiece piece)
        {
            var tile = board.TileOf(piece);
            List<Tile> tiles = new List<Tile>();

            var validTiles = new HexMovementHelper(board, piece, 1)
                .NorthEast(1)
                //.North(2, HexMovementHelper.IsEmpty, (b, c, t) => !piece.HasMoved)
                //.NorthEast(1, HexMovementHelper.CanCapture)
                //.NorthWest(1, HexMovementHelper.CanCapture)
                .GenerateTiles();

            return validTiles;
        }
    }
}
