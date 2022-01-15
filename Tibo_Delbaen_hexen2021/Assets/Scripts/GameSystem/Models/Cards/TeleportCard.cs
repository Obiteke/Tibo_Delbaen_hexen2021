using BoardSystem;
using DeckSystem.Utils;
using System.Collections.Generic;

namespace GameSystem.Models.Cards
{
    [CardName("Teleport")]
    public class TeleportCard : CardBase
    {
        public TeleportCard(Board<HexenPiece> board) : base(board)
        {

        }

        public override void OnMouseReleased(Tile playerTile, Tile focusedTile)
        {
            if (Tiles(playerTile, focusedTile).Contains(focusedTile))
                Board.Move(playerTile, focusedTile);
        }
        public override List<Tile> Tiles(Tile playerTile, Tile focusedTile)
        {
            List<Tile> tiles = new List<Tile>();

            if (Board.PieceAt(focusedTile) == null)
                tiles.Add(focusedTile);

            return tiles;
        }
    }
}
