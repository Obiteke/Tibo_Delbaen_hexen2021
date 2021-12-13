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

        private void OnValidate()
        {
            //if (_diameter <= 0)
            //    _diameter = 1;
        }


        public float [] PixelToHexPoint(float x, float z, float height)
        {
            var q = (Mathf.Sqrt(3)/3 * x - 1/ 3 * z) / height;
            var r = (2/3 * z) / height;
            var s = AxialToCube(q, r);

            return CubeRound(q, r, s);
        }
        //public static float[] PixelToPointyHex(float x, float z, float radius)
        //{
        //    float _sqr = Mathf.Sqrt(3f);
        //    var hex = new float[] { (_sqr / 3f * x - 0.333333f * z) / radius, 0.666666f * z / radius };
        //    float[] cube = AxialToCube(hex[0], hex[1]);
        //    float[] rounded = CubeRound(cube[0], cube[1], cube[2]);
        //    return rounded;
        //    //float[] axial = CubeToAxial(rounded[0], rounded[1], rounded[2]);
        //    //return axial;
        //}

        private float AxialToCube(float q, float r)
        {
            var s = -q - r;
            return s;
        }

        public float[] CubeRound(float x, float y, float z)
        {
            var q = Mathf.Round(x);
            var r = Mathf.Round(y);
            var s = Mathf.Round(z);

            var q_diff = Mathf.Abs(q - x);
            var r_diff = Mathf.Abs(r - y);
            var s_diff = Mathf.Abs(s - z);

            if (q_diff > r_diff && q_diff > s_diff)
                q = -r - s;
            else if (r_diff > s_diff)
                r = -q - s;
            else
                s = -q - r;

            return new float[] { q, r, s};
        }
        
    }
}