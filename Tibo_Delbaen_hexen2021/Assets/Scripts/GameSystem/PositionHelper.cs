using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem
{

    [CreateAssetMenu(menuName = "DAE/Position Helper")]
    public class PositionHelper : ScriptableObject
    {

        private void OnValidate()
        {
            if (_tileDimension <= 0)
                _tileDimension = 1;
        }

        [SerializeField]
        private float _tileDimension = 1;

        public (int x, int y) ToHexGridPostion(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        {
            var relativePosition = worldPosition - parent.position;

            var scaledRelativePosition = relativePosition / _tileDimension;

            var scaledBoardOffset = new Vector3(grid.Columns / 2.0f, 0, grid.Rows / 2.0f);
            scaledRelativePosition += scaledBoardOffset;

            var scaledHalfTileOffset = new Vector3(0.5f, 0, 0.5f);
            scaledRelativePosition -= scaledHalfTileOffset;

            var x = (int)scaledRelativePosition.x;
            var y = (int)scaledRelativePosition.z;

            return (x, y);
        }

        public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int x, int y)
        {
            var scaledRelativePosition = new Vector3(x, 0, y);

            var scaledHalfTileOffset = new Vector3(0.5f, 0, 0.5f);
            scaledRelativePosition += scaledHalfTileOffset;

            var scaledBoardOffset = new Vector3(grid.Columns / 2.0f, 0, grid.Rows / 2.0f);
            scaledRelativePosition -= scaledBoardOffset;

            var relativePosition = scaledRelativePosition * _tileDimension;
            var worldPosition = relativePosition + parent.position;

            return worldPosition;
        }
    }
}