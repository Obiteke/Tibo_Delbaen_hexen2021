using Hexen.BoardSystem;
using Hexen.PositionSystem;
using UnityEngine;

namespace Hexen.GameSystem
{
    class GameLoop : MonoBehaviour
    {

        [SerializeField]
        private PositionHelper _positionHelper;

        internal void DebugPosition(Tile tile)
        {

            //var (q, r, s) = _positionHelper.ToHexGridPostion(_grid, _boardParent, tile.transform.position);
            //Debug.Log($"Value of Tile {tile.name} is Q: {q} and R: {r} and S: {s}");
        }

        [SerializeField]
        private Transform _boardParent;

        //private SelectionManager<Piece> _selectionManager;
        private Grid<Position> _grid;
        //private Board<Position, Piece> _board;
        //private MoveManager<Piece> _moveManager;

        public void Start()
        {
            _grid = new Grid<Position>(3);
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
                float[] hexList = _positionHelper.PixelToHexPoint(tile.transform.position.x, tile.transform.position.z, 3f);
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