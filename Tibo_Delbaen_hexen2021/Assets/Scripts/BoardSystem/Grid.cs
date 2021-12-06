using Hexen.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hexen.BoardSystem
{
    public class Grid<TPosition>
    {

        public int Radius { get; }

        public Grid(int radius)
        {
            Radius = radius;
        }

        private MultiValueDictionary<(float q, float r, float s), TPosition> _positions = new MultiValueDictionary<(float, float, float), TPosition>();
        


        public void Register(float q, float r, float s, TPosition position)
        {
            _positions.Add((q, r, s), position);
//#if UNITY_EDITOR
//            if (x < 0 || x >= Columns)
//                throw new ArgumentException(nameof(x));
//
//            if (y < 0 || y >= Rows)
//                throw new ArgumentException(nameof(y));
//#endif

            //_positions.Add((x, y), position);
        }
    }
}
