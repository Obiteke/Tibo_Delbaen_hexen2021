using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultHexenPieceViewFactory", menuName = "GameSystem/HexenPieceView Factory")]
    public class HexenPieceViewFactory : ScriptableObject
    {

        [SerializeField]
        List<string> _movementNames = new List<string>();

        [SerializeField]
        PlayerView _prefab = null;

        [SerializeField]        
        private PositionHelper _positionHelper = null;

        public PlayerView CreateHexenPieceView(Board<HexenPiece> board, HexenPiece model, string movementName)
        {
            var hexenPieceView = Instantiate(_prefab);

            var tile = board.TileOf(model); 
            hexenPieceView.name = $"Spawned HexenPiece ( { movementName } )";
            hexenPieceView.Model = model;

            return hexenPieceView;
        }
    }
}
