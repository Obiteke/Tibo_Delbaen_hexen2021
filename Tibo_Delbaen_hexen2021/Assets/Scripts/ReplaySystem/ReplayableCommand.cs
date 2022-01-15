using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySystem
{
    public class ReplayManager
    {
        List<IReplayableCommand> _commands = new List<IReplayableCommand>();

        int _seekPosition = -1;

        public bool IsAtEnd => _commands.Count == _seekPosition + 1;

        public void Execute(IReplayableCommand command)
        {
            var wasAtEnd = (_commands.Count == _seekPosition + 1);
            _commands.Add(command);

            if (wasAtEnd)
                Forward();
        }

        public void Forward()
        {
            if (_commands.Count <= _seekPosition + 1)
                return;

            _seekPosition++;

            _commands[_seekPosition].Forward();
        }

        public void Backward()
        {
            if (_seekPosition < 0)
                return;

            _commands[_seekPosition].Backward();

            _seekPosition--;
        }
    }
}
