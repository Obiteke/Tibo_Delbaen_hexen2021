using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.States;
using GameSystem.Utils;
using MoveSystem;
using ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerMoveCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "Player";

        public PlayerMoveCommandProvider(PlayGameState playGameState, ReplayManager replayManager) : base(playGameState,
            //new PlayerMoveCommand(replayManager),
            new PlayerChargeCommand(replayManager),
            new PlayerSwipeCommand(replayManager),
            new PlayerPushbackCommand(replayManager),
            new PlayerTeleportCommand(replayManager))
        { }
    }
}
