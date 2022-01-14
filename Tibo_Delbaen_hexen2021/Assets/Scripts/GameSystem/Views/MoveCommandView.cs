using GameSystem.Models;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class MoveCommandView : MonoBehaviour, IPointerClickHandler
    {

        public IMoveCommand<HexenPiece> Model { get; set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameLoop.Instance.Select(Model);
        }
    }
}
