using BoardSystem;
using DeckSystem.Utils;
using GameSystem.MoveCommands;
using GameSystem.Utils;
using System.Collections.Generic;

namespace GameSystem.Models.Cards
{
    [CardName("Charge")]
    public class ChargeCard : CardBase
    {
        public ChargeCard(Board<HexenPiece> board) : base(board)
        {

        }
        public override void OnMouseReleased(Tile playerTile, Tile focusedTile)
        {
            List<Tile> tiles = Tiles(playerTile, focusedTile);

            if (!tiles.Contains(focusedTile))
                return;

            foreach (Tile tile in tiles)
            {
                if (Board.PieceAt(tile) == null)
                    continue;
                Board.Take(tile);
            }
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
        public override List<Tile> Tiles(Tile playerTile, Tile focusedTile)
        {
            var player = Board.PieceAt(playerTile);

            var _distance = HexagonHelper.Distance(
                playerTile.Position.X,
                playerTile.Position.Y,
                playerTile.Position.Z,
                focusedTile.Position.X,
                focusedTile.Position.Y,
                focusedTile.Position.Z
            );

            var validTiles = new HexMovementHelper(Board, player, Board.Radius * 2, focusedTile)
            //var validTiles = new HexMovementHelper(Board, player, (int)_distance, focusedTile)
                .NorthEast(1)
                .GenerateTiles();

            return validTiles;
        }
    }
}
