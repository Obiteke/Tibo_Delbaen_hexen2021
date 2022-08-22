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

        List<Tile> allBoardTiles = new List<Tile>();

        List<Tile> _keys = new List<Tile>();
        List<TPiece> _values = new List<TPiece>();

        public readonly int Radius;

        public bool HexTiles = false;

        public List<Tile> Tiles => _tiles.Values.ToList();
        public List<TPiece> Enemies { get; } = new List<TPiece>();
        public List<TPiece> Players { get; } = new List<TPiece>();

        public Board(int radius)
        {
            Radius = radius;

            HexTiles = radius != -1;
            InitTiles();
        }
        public void OnStart()
        {
            allBoardTiles = Tiles;
        } 

        public Tile TileAt(Position position)
        {
            if (_tiles.TryGetValue(position, out var tile))
                return tile;

            return null;
        }
        public List<Tile> TakeRandomTiles(int countOfRandomTiles)
        {
            
            List<Tile> randomTiles = new List<Tile>();

            var random = new System.Random();

            var nums = Enumerable.Range(0, allBoardTiles.Count).ToArray();
            
            // Shuffle the array
            for (int i = 0; i < countOfRandomTiles; ++i)
            {
                int randomIndex = random.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }

            for (int i = 0; i < countOfRandomTiles; i++)
            {
                //int tileNumberInList = random.Next(allBoardTiles.Count);
                //Debug.Log("hey this is the tile number in list " + nums[i]);
                //Debug.Log(nums[i]);
                randomTiles.Add(allBoardTiles[nums[i]]);
                allBoardTiles.RemoveAt(nums[i]);

            }

            return randomTiles;

            
            // Now your array is randomized and you can simply print them in order



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
        public void DestroyTile(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                //_tiles.Remove(tile.Position);
                tile.IsDestroyed = true;

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
        //public void DestroyTiles()
        //{
        //    _tiles.Remove();
        //}

        private bool IsValidPosition(int x, int y, int z)
        {
            return x + y + z == 0;
        }
    }
}
