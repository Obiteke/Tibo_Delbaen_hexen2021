using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Hexen.BoardSystem;
using Hexen.Utils;

namespace Hexen.GameSystem.Views
{
    public class TileView : MonoBehaviour
    {
        [SerializeField]
        PositionHelper _positionHelper = null;

        [SerializeField]
        Material _highlightMaterial = null;

        Material _originalMaterial;

        MeshRenderer _meshRenderer;

        Tile _model;

        public float Radius { get; set; }

        public Tile Model
        {
            get => _model;
            set
            {
                if (_model != null)
                    _model.HighlightStatusChanged -= ModelHighlightStatusChanged;

                _model = value;

                if (_model != null)
                    _model.HighlightStatusChanged += ModelHighlightStatusChanged;
            }
        }

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;

            GameLoop.Instance.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var board = GameLoop.Instance.Board;
            var boardPosition = _positionHelper.ToBoardPosition(transform.localPosition);
            var tile = board.TileAt(boardPosition);

            Model = tile;
        }

        private void ModelHighlightStatusChanged(object sender, EventArgs e)
        {
            if (Model.IsHighlighted)
                _meshRenderer.material = _highlightMaterial;
            else
                _meshRenderer.material = _originalMaterial;
        }
    }
}
