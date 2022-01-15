using BoardSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler, IDropHandler
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
            _meshRenderer = gameObject.transform.GetChild(0).GetChild(1).GetComponentInChildren<MeshRenderer>();
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

        internal Vector3 Size
        {
            set
            {
                transform.localScale = Vector3.one;

                var meshRenderer = GetComponentInChildren<MeshRenderer>();
                var meshSize = meshRenderer.bounds.size;

                var ratioX = value.x / meshSize.x;
                var ratioY = value.y / meshSize.y;
                var ratioZ = value.z / meshSize.z;

                transform.localScale = new Vector3(ratioX, ratioY, ratioZ);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameLoop.Instance.Select(Model);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            SingletonMonoBehaviour<GameLoop>.Instance.OnPointerEnterTile(eventData, _model);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            SingletonMonoBehaviour<GameLoop>.Instance.OnPointerExitTile(eventData, _model);
        }
        
        private void OnDestroy()
        {
            Model = null;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var activeCard = eventData.pointerDrag;
            if (activeCard == null && activeCard.GetComponent<CardView>() == null)
                return;
        
            SingletonMonoBehaviour<GameLoop>.Instance.OnCardReleased(_model, activeCard.GetComponent<CardView>().Model);
        }
    }
}
