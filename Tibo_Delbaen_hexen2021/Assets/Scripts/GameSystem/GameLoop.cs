using Hexen.BoardSystem;
using Hexen.PositionSystem;
using Hexen.CardSystem;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Hexen.GameSystem
{
    class GameLoop : MonoBehaviour
    {

        [SerializeField]
        private PositionHelper _positionHelper;

        internal void DebugPosition(Tile tile)
        {
            float[] list = _positionHelper.PixelToHexPoint(tile.transform.position.x, tile.transform.position.z, 1f);
            Debug.Log($"Value of Tile {tile.name} is Q: {list[0]} and R: {list[1]} and S: {list[2]}");
        }

        [SerializeField]
        private Transform _boardParent;

        //private SelectionManager<Piece> _selectionManager;
        private Grid<Position> _grid;

        //public Board<HexenPiece> Board { get; private set; }
        public Deck<CardBase> Deck { get; private set; }
        public Hand<CardBase> Hand { get; private set; }

        [SerializeField]
        private int _gridSize = 3;
        //private Board<Position, Piece> _board;
        //private MoveManager<Piece> _moveManager;

        public void Start()
        {
            //CreateDeck();
            Hand = Deck.DealHand(5);
            _grid = new Grid<Position>(_gridSize);
            ConnectGrid(_grid);
            //
            //_board = new Board<Position, Piece>();
            //_selectionManager = new SelectionManager<Piece>();
            //ConnectPiece(_selectionManager, _grid, _board);
            //
            //_moveManager = new MoveManager<Piece>(_board, _grid);


            //_selectionManager.Selected += (s, e) =>
            //{
            //    //e.SelectableItem.Highlight = true;
            //
            //    var positions = _moveManager.ValidPositionFor(e.SelectableItem);
            //    foreach (var position in positions)
            //    {
            //        position.Activate();
            //    }
            //};
            //
            //_selectionManager.Deselected += (s, e) =>
            //{
            //    var positions = _moveManager.ValidPositionFor(e.SelectableItem);
            //    foreach (var position in positions)
            //    {
            //        position.Deactivate();
            //    }
            //};
        }

        //private void CreateDeck()
        //{
        //    Deck = new Deck<CardBase>();
        //
        //    Dictionary<string, CardBase> _cards = new Dictionary<string, CardBase>() {
        //    { "Charge", new ChargeCard(Board) },
        //    { "Push", new PushCard(Board) },
        //    { "Swipe", new SwipeCard(Board) },
        //    { "Teleport", new TeleportCard(Board) }
        //};
        //
        //    for (int i = 0; i < _cards.Count; i++)
        //    {
        //        Deck.RegisterCard(_cards.Keys.ElementAt(i), _cards.Values.ElementAt(i));
        //        //Debug.Log(_cards.Keys.ElementAt(i));
        //    }
        //}

        //public void DeselectAll()
        //{
        //    _selectionManager.DeselectAll();
        //}

        private void ConnectGrid(Grid<Position> grid)
        {
            var tiles = FindObjectsOfType<Tile>();
            foreach (var tile in tiles)
            {
                var position = new Position();
                tile.Model = position;
                float[] hexList = _positionHelper.PixelToHexPoint(tile.transform.position.x, tile.transform.position.z, 0.577f);
                //var (q, r, s) = _positionHelper.ToHexGridPostion(grid, _boardParent, tile.transform.position);
                
                grid.Register(hexList[0], hexList[1], hexList[2], position);
        
                tile.gameObject.name = $"Tile ({hexList[0]},{hexList[1]},{hexList[2]})";
            }
        }

        //private void ConnectPiece(SelectionManager<Piece> selectionManager, Grid<Position> grid, Board<Position, Piece> board)
        //{
        //    var pieces = FindObjectsOfType<Piece>();
        //    foreach (var piece in pieces)
        //    {
        //        var (x, y) = _positionHelper.ToGridPostion(grid, _boardParent, piece.transform.position);
        //        if (grid.TryGetPositionAt(x, y, out var position))
        //        {
        //            piece.Clicked += (s, e) =>
        //            {
        //                selectionManager.DeselectAll();
        //                selectionManager.Toggle(s as Piece);
        //            };
        //
        //            board.Place(piece, position);
        //        }
        //    }
        //}
    }
}