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

        public int Rows { get; }

        public int Columns { get; }

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        private MultiValueDictionary<(int x, int y), TPosition> _positions = new MultiValueDictionary<(int, int), TPosition>();
        

        //public bool TryGetPositionAt(int x, int y, out TPosition position)
         //   => _positions.TryGetValue((x, y), out position);


        //public bool TryGetCoordinateOf(TPosition position, out (int x, int y) coordinate)
          //  => _positions.TryGetKey(position, out coordinate);

        public void Register(int x, int y, TPosition position)
        {

#if UNITY_EDITOR
            if (x < 0 || x >= Columns)
                throw new ArgumentException(nameof(x));

            if (y < 0 || y >= Rows)
                throw new ArgumentException(nameof(y));
#endif

            //_positions.Add((x, y), position);
        }
    }
}
