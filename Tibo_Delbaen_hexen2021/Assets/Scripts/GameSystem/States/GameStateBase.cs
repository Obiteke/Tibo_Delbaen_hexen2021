using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace GameSystem.States
{
    public abstract class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { set; get; }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }


        public virtual void OnCardReleased(Tile hoverTile, string card) { }
        public virtual void OnCardDragStart(string card) { }
        public virtual void OnCardTileFocused(Tile hoverTile, bool entered) { }
        public virtual void OnPointerExitTile(PointerEventData eventData, Tile model) { }
        public virtual void OnPointerEnterTile(PointerEventData eventData, Tile model) { }

        public virtual void Select(HexenPiece hexenPiece) { }

        public virtual void Select(Tile tile) { }

        public virtual void Select(IMoveCommand<HexenPiece> moveCommand) { }


        public virtual void Forward() { }
        public virtual void Backward() { }
    }
}
