using BoardSystem;
using GameSystem.Models;
using GameSystem.Utils;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public class HexMovementHelper
    {
        public Dictionary<Direction.HexCardinal, Position> _directions = new Dictionary<Direction.HexCardinal, Position>()
        {
            { Direction.HexCardinal.NorthEast, new Position { X = 1, Z = -1 } },
            { Direction.HexCardinal.East, new Position { X = 1, Z = 0 } },
            { Direction.HexCardinal.SouthEast, new Position { X = 0, Z = 1 } },
            { Direction.HexCardinal.SouthWest, new Position { X = -1, Z = 1 } },
            { Direction.HexCardinal.West, new Position { X = -1, Z = 0 } },
            { Direction.HexCardinal.NorthWest, new Position { X = 0, Z = -1 } },
            { Direction.HexCardinal.Center, new Position { X = 0, Z = 0 } }
        };

        public delegate bool Validator(Board<HexenPiece> board, HexenPiece hexenPiece, Tile toTile);

        readonly Board<HexenPiece> _board;
        readonly HexenPiece _hexenPiece;
        readonly Tile _tile;
        private readonly Tile _focusedTile;
        readonly List<Tile> _tiles = new List<Tile>();
        private readonly int _radius = 1;

        public HexMovementHelper(Board<HexenPiece> board, HexenPiece hexenPiece)
        {
            _board = board;
            _hexenPiece = hexenPiece;
        }

        public HexMovementHelper(Board<HexenPiece> board, Tile tile, int radius)
        {
            _board = board;
            _tile = tile;
            _radius = radius;
        }
        public HexMovementHelper(Board<HexenPiece> board, HexenPiece hexenPiece, int radius) : this(board, hexenPiece)
        {
            _radius = radius;
        }
        public HexMovementHelper(Board<HexenPiece> board, HexenPiece hexenPiece, int radius, Tile focusedTile) : this(board, hexenPiece, radius)
        {
            _focusedTile = focusedTile;
        }
        public HexMovementHelper(Board<HexenPiece> board, HexenPiece hexenPiece, Tile focusedTile, int radius) : this(board, focusedTile, radius)
        {
            _focusedTile = focusedTile;
        }

        public Position Offset(Position fromPosition, Position offset, int radius = 1)
        {
            offset.X *= radius;
            offset.Y *= radius;
            offset.Z *= radius;
            offset.X += fromPosition.X;
            offset.Y += fromPosition.Y;
            offset.Z += fromPosition.Z;
            return offset;
        }
        //public HexMovementHelper North(int steps = int.MaxValue, params Validator[] validators)
        //{
        //    return Collect(0, 1, steps, validators);
        //}
        public HexMovementHelper Radius(int steps = int.MaxValue, params Validator[] validators)
        {
            return CollectPF(1, -1, steps, validators);
        }
        public HexMovementHelper NorthEast(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, -1, steps, validators);
        }
        public HexMovementHelper East(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(1, 0, steps, validators);
        }
        public HexMovementHelper SouthEast(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, 1, steps, validators);
        }
        //public HexMovementHelper South(int steps = int.MaxValue, params Validator[] validators)
        //{
        //    return Collect(0, -1, steps, validators);
        //}
        public HexMovementHelper SouthWest(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 1, steps, validators);
        }
        public HexMovementHelper West(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(-1, 0, steps, validators);
        }
        public HexMovementHelper NorthWest(int steps = int.MaxValue, params Validator[] validators)
        {
            return Collect(0, -1, steps, validators);
        }

        public HexMovementHelper CollectPF(int x, int y, int steps = int.MaxValue, params Validator[] validators)
        {
            Position startTile = _tile.Position;

            Tile tile;
            for (int i = 0; i < 7; i++)
            {
                List<Tile> _rowTiles = new List<Tile>();
                for (int j = 1; j <= _radius; j++)
                {
                    _directions.TryGetValue((Direction.HexCardinal)i, out Position direction);
                    float[] offset = HexagonHelper.AxialToCube(direction.X, direction.Z);
                    direction.X = (int)offset[0];
                    direction.Y = (int)offset[1];
                    direction.Z = (int)offset[2];
                    var _pos = Offset(startTile, direction, j);
                    tile = _board.TileAt(_pos);

                    if (tile != null)
                    {
                        _rowTiles.Add(tile);
                        _tiles.Add(tile);
                    }
                }
            }
            return this;
        }
        public HexMovementHelper Collect(int x, int y, int steps = int.MaxValue, params Validator[] validators)
        {
            Position startTile = _board.TileOf(_hexenPiece).Position;

            //Tile tile = _board.TileAt(_pos);
            //if (tile != null)
            //{
            //    _tiles.Add(tile);
            //}
            Tile tile;
            for (int i = 0; i < 6; i++)
            {
                List<Tile> _rowTiles = new List<Tile>();
                for (int j = 1; j <= _radius; j++)
                {
                    _directions.TryGetValue((Direction.HexCardinal)i, out Position direction);
                    float[] offset = HexagonHelper.AxialToCube(direction.X, direction.Z);
                    direction.X = (int)offset[0];
                    direction.Y = (int)offset[1];
                    direction.Z = (int)offset[2];
                    var _pos = Offset(startTile, direction, j);
                    tile = _board.TileAt(_pos);

                    if (tile != null)
                    {
                        _rowTiles.Add(tile);
                        _tiles.Add(tile);
                    }
                }
                if (_rowTiles.Contains(_focusedTile))
                {
                    _tiles.Clear();
                    _tiles.AddRange(_rowTiles);
                    return this;
                }
            }
            return this;
        }

        public List<Tile> GenerateTiles()
        {
            return new List<Tile>(_tiles);
        }
        public static bool CanCapture(Board<HexenPiece> board, HexenPiece hexenPiece, Tile toTile)
        {
            var other = board.PieceAt(toTile);
            return other != null && other != hexenPiece;
        }
        public static bool IsEmpty(Board<HexenPiece> board, HexenPiece hexenPiece, Tile toTile)
        {
            var other = board.PieceAt(toTile);
            return other == null;
        }
        public static bool IsFocused(Board<HexenPiece> board, HexenPiece hexenPiece, Tile toTile)
        {
            var other = board.PieceAt(toTile);
            return other == null;
        }
    }
}
