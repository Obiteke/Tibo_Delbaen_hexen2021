using UnityEngine;
using BoardSystem;
using GameSystem.Utils;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private float _radius = 0.5f;

        public float Radius => _radius;


        public Position ToBoardPosition(Vector3 localPosition)
        {
            float[] hex = HexagonHelper.PixelToPointyHex(localPosition.x, localPosition.z, _radius);
            return new Position() { X = (int)hex[0], Y = (int)hex[1], Z = (int)hex[2] };
        }

        public Vector3 ToWorldPosition(Transform transform, Position position)
        {
            Vector3 localPosition = ToLocalPosition(position);
            return transform.localToWorldMatrix * localPosition;
        }
        public Vector3 ToLocalPosition(Position boardPosition)
        {
            float[] pos = HexagonHelper.PointyHexToPixel(boardPosition.X, boardPosition.Z, _radius);
            return new Vector3(pos[0], 0f, pos[1]);
        }

    }
}
