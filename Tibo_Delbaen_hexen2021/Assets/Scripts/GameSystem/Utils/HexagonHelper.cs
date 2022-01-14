using UnityEngine;

namespace GameSystem.Utils
{
    public class HexagonHelper
    {
        public static float[] AxialToCube(float q, float r)
        {
            return new float[] { q, -q - r, r };
        }

        public static float[] CubeToAxial(float x, float y, float z)
        {
            return new float[] { x, z };
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

        public static float[] CubeRound(float x, float y, float z)
        {
            float rx = Mathf.Round(x);
            float ry = Mathf.Round(y);
            float rz = Mathf.Round(z);
            float xDiff = Mathf.Abs(rx - x);
            float yDiff = Mathf.Abs(ry - y);
            float zDiff = Mathf.Abs(rz - z);
            if (xDiff > yDiff && xDiff > zDiff)
                rx = -ry - rz;
            else if (yDiff > zDiff)
                ry = -rx - rz;
            else
                rz = -rx - ry;
            return new float[3] { rx, ry, rz };
        }

        public static float[] PixelToPointyHex(float x, float z, float radius)
        {
            float _sqr = Mathf.Sqrt(3f);
            var hex = new float[] { (_sqr / 3f * x - 0.333333f * z) / radius, 0.666666f * z / radius };
            float[] cube = AxialToCube(hex[0], hex[1]);
            float[] rounded = CubeRound(cube[0], cube[1], cube[2]);
            return rounded;
        }

        public static float Distance(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            float x = Mathf.Abs(x1 - x2);
            float y = Mathf.Abs(y1 - y2);
            float z = Mathf.Abs(z1 - z2);

            float _result = (x + y + z) / 2;
            return _result;
        }
    }
}
