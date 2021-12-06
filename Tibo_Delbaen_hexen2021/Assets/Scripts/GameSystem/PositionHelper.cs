using Hexen.PositionSystem;
using Hexen.BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen.GameSystem
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

        public (int q, int r, int s) ToHexGridPostion(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        {
            var relativePosition = worldPosition - parent.position;

            var scaledRelativePosition = relativePosition / _tileDimension;

            var q = (int)scaledRelativePosition.x;
            var r = (int)scaledRelativePosition.z;
            var s = -q - r;

            return (q, r, s);
        }

        public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int x, int y)
        {
            var scaledRelativePosition = new Vector3(x, 0, y);

            var relativePosition = scaledRelativePosition * _tileDimension;
            var worldPosition = relativePosition + parent.position;

            return worldPosition;
        }
    }
}