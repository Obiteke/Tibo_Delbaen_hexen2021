using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System.Collections.Generic;
using StateSystem;

namespace GameSystem.States
{
    public class EndGameState : GameStateBase
    {

        StateMachine<GameStateBase> _stateMachine;

        public EndGameState(StateMachine<GameStateBase> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        //public override void StartGame()
        //{
        //    _stateMachine.MoveTo(GameStates.Play);
        //}
        public override void EndGame()
        {
            base.EndGame();
        }
    }
}
