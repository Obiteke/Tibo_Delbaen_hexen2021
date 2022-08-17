using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class PlayerView : ObjectView, IPointerClickHandler
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        string _movementName = null;

        private Transform _boardViewTransform;

        HexenPiece _model;
        public string MovementName => _movementName;

        public HexenPiece Model
        {
            get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.PieceMoved -= ModelMoved;
                    _model.PieceTaken -= ModelTaken;
                }

                _model = value;

                if (_model != null)
                {
                    _model.PieceMoved += ModelMoved;
                    _model.PieceTaken += ModelTaken;
                }
            }
        }
        private void ModelTaken(object sender, EventArgs e)
        {
            Debug.Log("player taken");
            FindObjectOfType<GameLoop>().EndGame();

            ////GameLoop.Instance.Board.Pieces.Remove(Model);
            ////GameLoop.Instance.PieceViews.Remove(gameObject);
            Destroy(gameObject);
        }
        private void ModelMoved(object sender, PieceMovedEventArgs e)
        {
            var worldPosition = _positionHelper.ToWorldPosition(_boardViewTransform, e.To.Position);
            transform.position = worldPosition;
        }
        public override void Moved(Tile fromTile, Tile toTile)
        {
            Vector3 worldPosition = _positionHelper.ToWorldPosition(_boardViewTransform, toTile.Position);
            transform.position = worldPosition;
        }
        private void Start()
        {
            _boardViewTransform = FindObjectOfType<BoardView>().transform;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            var board = GameLoop.Instance;
            
            board.Select(Model);
        }
        public override void Taken() { }
    }
}
