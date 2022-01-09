
using Hexen.BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hexen.GameSystem
{

    [CreateAssetMenu(menuName = "DAE/Position Helper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private float _radius = 0.5f;

        public float Radius => _radius;

        public Position ToBoardPosition(Vector3 localPosition)
        {
            float[] hex = PixelToHexPoint(localPosition.x, localPosition.z, _radius);
            return new Position() { X = (int)hex[0], Y = (int)hex[1], Z = (int)hex[2] };
        }

        private float [] PixelToHexPoint(float x, float z, float radius)
        {
            float _sqr = Mathf.Sqrt(3f);
            var hex = new float[] { (_sqr / 3f * x - 0.3333333f * z) / radius, 0.6666666f * z / radius };
            float[] cube = AxialToCube(hex[0], hex[1]);
            float[] rounded = CubeRound(cube[0], cube[1], cube[2]);

            return rounded;
        }

        public static float[] AxialToCube(float q, float r)
        {
            return new float[] { q, -q - r, r };
        }

        public float[] CubeRound(float x, float y, float z)
        {
            var rx = Mathf.Round(x);
            var ry = Mathf.Round(y);
            var rz = Mathf.Round(z);

            var xDiff = Mathf.Abs(rx - x);
            var yDiff = Mathf.Abs(ry - y);
            var zDiff = Mathf.Abs(rz - z);

            if (xDiff > yDiff && xDiff > zDiff)
                rx = -ry - rz;
            else if (yDiff > zDiff)
                ry = -rx - rz;
            else
                rz = -rx - ry;

            return new float[3] { rx, ry, rz};
        }
        public static float[] PointyHexToPixel(float q, float r, float radius)
        {
            float _sqr = Mathf.Sqrt(3f);
            float w = radius * _sqr;
            float h = 2 * radius * .75f;
            float sin60 = _sqr * .5f;
            float[] result = new float[] { radius * (_sqr * q + sin60 * r), h * r };
            return result;
        }
        public Vector3 ToWorldPosition(Transform transform, Position position)
        {
            Vector3 localPosition = ToLocalPosition(position);
            return transform.localToWorldMatrix * localPosition;
        }
        public Vector3 ToLocalPosition(Position boardPosition)
        {
            float[] pos = PointyHexToPixel(boardPosition.X, boardPosition.Z, _radius);
            return new Vector3(pos[0], 0f, pos[1]);
        }
    }
}