using BoardSystem;
using System.Collections.Generic;

namespace GameSystem.Models.Cards
{
    public abstract class CardBase
    {
        protected Board<HexenPiece> Board;

        public CardBase(Board<HexenPiece> board)
        {
            Board = board;
        }

        public abstract void OnMouseReleased(Tile playerTile, Tile focusedTile);
        public abstract List<Tile> Tiles(Tile playerTile, Tile focusedTile);
    }
}
