using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System.Collections.Generic;
using StateSystem;

namespace GameSystem.States
{
    public class StartGameState : GameStateBase
    {

        StateMachine<GameStateBase> _stateMachine;

        public StartGameState(StateMachine<GameStateBase> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void StartGame()
        {
            _stateMachine.MoveTo(GameStates.Play);
            _stateMachine.CurrentState.OnEnter();
        }
    }
}
