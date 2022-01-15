using BoardSystem;
using DeckSystem.Utils;
using GameSystem.MoveCommands;
using System.Collections.Generic;

namespace GameSystem.Models.Cards
{
    [CardName("Push")]
    public class PushCard : CardBase
    {
        public PushCard(Board<HexenPiece> board) : base(board)
        {

        }
        internal Position CalculateTileDifference(Position playerTile, Position enemyTile)
        {
            return new Position
            {
                X = enemyTile.X - (playerTile.X - enemyTile.X),
                Y = enemyTile.Y - (playerTile.Y - enemyTile.Y),
                Z = enemyTile.Z - (playerTile.Z - enemyTile.Z)
            };
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

                var newPosition = CalculateTileDifference(playerTile.Position, tile.Position);

                if (Board.TileAt(newPosition) == null)
                    Board.Take(tile);

                Board.Move(tile, Board.TileAt(newPosition));
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

            var validTiles = new HexMovementHelper(Board, player, 1)
                .NorthEast(1)
                .GenerateTiles();

            int index = validTiles.IndexOf(focusedTile);
            if (index != -1)
            {
                int _count = validTiles.Count - 1;
                return new List<Tile>
                {
                    validTiles[(index - 1 < 0 ? _count : index - 1)],
                    validTiles[index],
                    validTiles[(index + 1 > _count ? 0 : index + 1)],
                };
            }

            return validTiles;
        }
    }
}
