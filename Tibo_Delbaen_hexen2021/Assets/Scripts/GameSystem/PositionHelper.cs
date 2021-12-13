using Hexen.PositionSystem;
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
        public float [] PixelToHexPoint(float x, float z, float size)
        {
            float _sqr = Mathf.Sqrt(3f);
            var hex = new float[] { (_sqr / 3f * x - 0.3333333f * z) / size, 0.6666666f * z / size };
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

            return new float[] { rx, ry, rz};
        }
        
    }
}