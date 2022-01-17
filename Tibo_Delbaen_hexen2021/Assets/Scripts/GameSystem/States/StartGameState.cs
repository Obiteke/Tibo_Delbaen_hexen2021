using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System.Collections.Generic;

namespace GameSystem.States
{
    public class StartGameState : GameStateBase
    {
        IMoveCommand<HexenPiece> _currentMoveCommand;
        //ChessPiece _selectedChessPiece = null;
        HexenPiece _selectedHexenPiece = null;
        Tile _selectedTile = null;
        MoveManager<HexenPiece> _moveManager;
        Board<HexenPiece> _board;

        public bool IsLightTurn { get; internal set; } = true;
        public HexenPiece SelectedHexenPiece => _selectedHexenPiece;
        public Board<HexenPiece> Board => _board;
        public StartGameState(Board<HexenPiece> board, MoveManager<HexenPiece> moveManager)
        {
            _moveManager = moveManager;
            _board = board;
        }

        public override void OnEnter()
        {
            
           _moveManager.DeActivate();
           
           _currentMoveCommand = null;
           _selectedHexenPiece = null;
           
           _moveManager.MoveCommandProviderChanged -= OnMoveCommandProviderChanged;
            
        }

        //public override void Select(HexenPiece hexenPiece)
        //{
        //    if (hexenPiece == null || hexenPiece == _selectedHexenPiece)
        //        return;
        //
        //    //if (hexenPiece != null/* && hexenPiece.IsLight != IsLightTurn*/)
        //    //{
        //    //    var tile = _board.TileOf(hexenPiece);
        //    //    Select(tile);
        //    //}
        //    //else
        //    //{
        //    _moveManager.DeActivate();
        //
        //    _currentMoveCommand = null;
        //
        //    _selectedHexenPiece = hexenPiece;
        //
        //    _moveManager.ActivateFor(_selectedHexenPiece);
        //    //}
        //}

        //public override void Select(Tile tile)
        //{
        //    //if (_selectedHexenPiece != null && _currentMoveCommand != null)
        //    //{
        //    //    var tiles = _currentMoveCommand.Tiles(_board, _selectedHexenPiece);
        //
        //    //    if (tiles.Contains(tile))
        //    //    {
        //    //        _board.UnHighlight(_currentMoveCommand.Tiles(_board, _selectedHexenPiece));
        //
        //    //        _currentMoveCommand.Execute(_board, _selectedHexenPiece, tile);
        //
        //    //        _moveManager.DeActivate();
        //
        //    //        _selectedHexenPiece = null;
        //    //        _currentMoveCommand = null;
        //
        //    //        IsLightTurn = !IsLightTurn;
        //    //    }
        //    //}
        //    if (_selectedTile != null)
        //    {
        //        _board.UnHighlight(new List<Tile> { tile });
        //        _selectedTile = null;
        //    }
        //    else
        //    {
        //        _selectedTile = tile;
        //        _board.Highlight(new List<Tile> { tile });
        //    }
        //}

        //public override void Select(IMoveCommand<HexenPiece> moveCommand)
        //{
        //    if (_currentMoveCommand != null)
        //        _board.UnHighlight(_currentMoveCommand.Tiles(_board, _selectedHexenPiece));
        //
        //    _currentMoveCommand = moveCommand;
        //
        //    if (_currentMoveCommand != null)
        //        _board.Highlight(_currentMoveCommand.Tiles(_board, _selectedHexenPiece));
        //}

        //public override void Backward()
        //{
        //    StateMachine.MoveTo(GameStates.Replay);
        //}

        private void OnMoveCommandProviderChanged(object sender, MoveCommandProviderChangedEventArgs<HexenPiece> e)
        {
            if (_currentMoveCommand == null)
                return;
        
            var tiles = _currentMoveCommand.Tiles(_board, _selectedHexenPiece);
            _board.UnHighlight(tiles);
        }
    }
}
