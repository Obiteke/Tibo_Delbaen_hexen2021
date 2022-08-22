using BoardSystem;
using DeckSystem.Utils;
using GameSystem.MoveCommands;
using System.Collections.Generic;

namespace GameSystem.Models.Cards
{
    [CardName("Rain")]
    public class RainCard : CardBase
    {
        public RainCard(Board<HexenPiece> board) : base(board)
        {

        }
        public override void OnMouseReleased(Tile playerTile, Tile focusedTile)
        {
            List<Tile> tiles = Tiles(playerTile, focusedTile);

            if (!tiles.Contains(focusedTile))
                return;

            tiles.Remove(focusedTile);

            //tiles.Add(Board.TakeRandomTiles(3));
            tiles = Board.TakeRandomTiles(3);
            foreach (Tile tile in tiles)
            {
                if (Board.PieceAt(tile) == null)
                    continue;
                Board.Take(tile);
                //Board.TileAt(tile.Position);
            }
                Board.DestroyTile(tiles);
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
            //var player = Board.PieceAt(playerTile);
            //var validTiles = new HexMovementHelper(Board, player, focusedTile, 20)
            //    .Radius(5)
            //    .GenerateTiles();

            List<Tile> tiles = new List<Tile>();

            if (Board.PieceAt(focusedTile) == null)
                tiles.Add(focusedTile);


            return tiles;



            //int index = validTiles.IndexOf(focusedTile);
            //if (index != -1)
            //{
            //    int _count = validTiles.Count - 1;
            //    return new List<Tile>
            //    {
            //        validTiles[(index - 1 < 0 ? _count : index - 1)],
            //        validTiles[index],
            //        validTiles[(index + 1 > _count ? 0 : index + 1)],
            //    };
            //}



            //return validTiles;
        }
    }
}
