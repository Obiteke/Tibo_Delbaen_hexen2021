using System.Collections;
using System;
using System.Collections.Generic;
using Hexen.Commons;
using UnityEngine;

namespace Hexen.BoardSystem
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
        
        private Grid<Position> _grid;
        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;

        //Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();
        private BidirectionalDictionary<Position, Tile> _tiles
                        = new BidirectionalDictionary<Position, Tile>();


        List<Tile> _keys = new List<Tile>();
        List<TPiece> _values = new List<TPiece>();

        public readonly int Radius;

        public bool HexTiles = false;

        //public List<Tile> Tiles => _tiles.Values.ToList();
        //public List<TPiece> Enemies { get; } = new List<TPiece>();

        public Board(int radius)
        {
            Radius = radius;

            HexTiles = radius != -1;

            _grid = new Grid<Position>();
            //ConnectTile(_grid);
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
        public void Register(Position position, Tile tile)
        {
            _tiles.Add(position, tile);
        }
        //private void ConnectTile(Grid<Position> grid)
        //{
        //    var tiles = FindObjectsOfType<Tile>();
        //    foreach (var tile in tiles)
        //    {
        //        var position = new Position();
        //        //tile.Model = position;
        //        float[] hexList = _positionHelper.PixelToHexPoint(tile.transform.position.x, tile.transform.position.z, 0.577f);
        //        //var (q, r, s) = _positionHelper.ToHexGridPostion(grid, _boardParent, tile.transform.position);
        //        
        //        grid.Register(position, hexList[0], hexList[1], hexList[2]);
        //
        //        tile.gameObject.name = $"Tile ({hexList[0]},{hexList[1]},{hexList[2]})";
        //    }
        //}
    }
}
