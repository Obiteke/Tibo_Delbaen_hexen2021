using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/TileView Factory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField]
        TileView _hexTileView = null;

        [SerializeField]
        PositionHelper _positionHelper = null;

        public TileView CreateTileView(Board<HexenPiece> board, Tile tile, Transform parent)
        {
            var position = _positionHelper.ToLocalPosition(tile.Position);

            var prefab = _hexTileView;

            var tileView = GameObject.Instantiate<TileView>(prefab, position, Quaternion.identity, parent);

            tileView.Radius = _positionHelper.Radius;

            tileView.name = $"Tile {tile.Position.X}, {tile.Position.Y}, {tile.Position.Z}";

            tileView.Model = tile;

            return tileView;
        }
    }
}
