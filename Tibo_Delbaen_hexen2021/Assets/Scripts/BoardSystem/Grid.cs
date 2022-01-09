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
        public Grid()
        {

        }

        //private MultiValueDictionary<TPosition, (float q, float r, float s)> _positions 
        //                = new MultiValueDictionary<TPosition ,(float q, float r, float s)>();

        private BidirectionalDictionary<TPosition, (float q, float r, float s)> _positions
                        = new BidirectionalDictionary<TPosition, (float q, float r, float s)>();

        public bool TryGetPositionAt(float q, float r, float s, out TPosition position)
            => _positions.TryGetKey((q, r, s), out position);

        public bool TryGetCoordinateAt(TPosition position, out (float q, float r, float s) coordinate)
             => _positions.TryGetValue(position, out coordinate);

        public void Register(TPosition position ,float q, float r, float s)
        {
            _positions.Add(position, (q, r, s));
        }
    }
}
