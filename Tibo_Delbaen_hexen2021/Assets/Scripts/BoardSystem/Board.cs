using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen.BoardSystem
{
    public class Board : MonoBehaviour
    {
        private int _gridSize = 3;
        [SerializeField]
        private GameObject _tileObject;

        // Start is called before the first frame update
        void Start()
        {
            CreateBoard();
        }
        private void CreateBoard()
        {
            for (int x = -_gridSize; x < _gridSize; x++)
            {
                for (int y = -_gridSize; y < _gridSize; y++)
                {
                    for (int z = -_gridSize; z < _gridSize; z++)
                    {
                        if (IsValidPosition(x, y, z))
                        {
                            Instantiate(_tileObject, new Vector3(x, 0, z),Quaternion.identity);
                        }
                    }
                }
            }
        }
        private bool IsValidPosition(int x, int y, int z)
        {
            return x + y + z == 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
