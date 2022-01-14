using GameSystem.Models;
using GameSystem.States;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    public abstract class AbstractMoveCommandProvider : IMoveCommandProvider<HexenPiece>
    {
        private readonly PlayGameState _playGameState;
        List<IMoveCommand<HexenPiece>> _commands = new List<IMoveCommand<HexenPiece>>();

        public AbstractMoveCommandProvider(PlayGameState playGameState, params IMoveCommand<HexenPiece>[] commands)
        {
            _commands = commands.ToList();
            _playGameState = playGameState;
        }

        public List<IMoveCommand<HexenPiece>> Commands()
        {
            return _commands.Where((command) => command.CanExecute(_playGameState.Board, _playGameState.SelectedHexenPiece)).ToList();
        }
    }
}
