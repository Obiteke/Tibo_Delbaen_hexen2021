using ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.States
{
    public class ReplayGameState : GameStateBase
    {
        private readonly ReplayManager _replayManager;

        public ReplayGameState(ReplayManager replayManager)
        {
            _replayManager = replayManager;
        }

        public override void OnEnter()
        {
            _replayManager.Backward();
        }

        public override void Backward()
        {
            _replayManager.Backward();
        }

        public override void Forward()
        {
            _replayManager.Forward();
            if (_replayManager.IsAtEnd)
                StateMachine.MoveTo(GameStates.Play);
        }
    }
}
