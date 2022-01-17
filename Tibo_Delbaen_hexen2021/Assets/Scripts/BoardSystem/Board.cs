using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace BoardSystem
{
    public class PiecePlacedEventArgs<TPiece> : EventArgs where TPiece : class, IPiece
    {
        public TPiece Piece { get; }

        public PiecePlacedEventArgs(TPiece piece)
        {
            Piece = piece;
        }
    }

    public class Board<TPiece> where TPiece : class, IPiece
    {
        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;

        Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();

        List<Tile> _keys = new List<Tile>();
        List<TPiece> _values = new List<TPiece>();

        public readonly int Radius;

        public bool HexTiles = false;

        public List<Tile> Tiles => _tiles.Values.ToList();
        public List<TPiece> Enemies { get; } = new List<TPiece>();
        //public List<TPiece> Pieces { get; } = new List<TPiece>();

        public Board(int radius)
        {
            Radius = radius;

            HexTiles = radius != -1;
            InitTiles();
        }

        public Tile TileAt(Position position)
        {
            if (_tiles.TryGetValue(position, out var tile))
                return tile;

            return null;
        }
        public TPiece PieceAt(Tile tile)
        {
            var idx = _keys.IndexOf(tile);
            if (idx == -1)
                return default;

            return _values[idx];
        }
        public Tile TileOf(TPiece piece)
        {
            var idx = _values.IndexOf(piece);
            if (idx == -1)
                return null;

            return _keys[idx];
        }

        //public bool TakeTileOf(Tile fromTile)
        //{
        //    return true;
        //}

        public TPiece Take(Tile fromTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return null;

            var piece = _values[idx];

            _values.RemoveAt(idx);
            _keys.RemoveAt(idx);

            piece.Taken();

            return piece;
        }
        public void Move(Tile fromTile, Tile toTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return;

            var toPiece = PieceAt(toTile);
            if (toPiece != null)
                return;

            _keys[idx] = toTile;

            var piece = _values[idx];
            piece.Moved(fromTile, toTile);
        }
        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile))
                return;

            if (_values.Contains(piece))
                return;

            _keys.Add(toTile);
            _values.Add(piece);

            OnPiecePlaced(new PiecePlacedEventArgs<TPiece>(piece));
        }

        public void Highlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = true;
            }
        }
        public void UnHighlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = false;
            }
        }
        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> args)
        {

            EventHandler<PiecePlacedEventArgs<TPiece>> handler = PiecePlaced;
            handler?.Invoke(this, args);
        }

        private void InitTiles()
        {
            for (int x = -Radius; x <= Radius; x++)
            {
                for (int y = -Radius; y <= Radius; y++)
                {
                    for (int z = -Radius; z <= Radius; z++)
                    {
                        if (IsValidPosition(x, y, z))
                        {
                            _tiles.Add(new Position { X = x, Y = y, Z = z }, new Tile(x, y, z));
                        }
                    }
                }
            }
        }

        public void RemoveTile(Tile tile)
        {
            _tiles.Remove(tile.Position);
        }

        private bool IsValidPosition(int x, int y, int z)
        {
            return x + y + z == 0;
        }
    }
}
