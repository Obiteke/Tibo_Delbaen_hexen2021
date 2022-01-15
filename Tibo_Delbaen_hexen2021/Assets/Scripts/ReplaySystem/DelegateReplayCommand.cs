using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySystem
{
    public class DelegateReplayCommand : IReplayableCommand
    {
        Action _forward;
        Action _backward;

        public DelegateReplayCommand(Action forward, Action backward)
        {
            _forward = forward;
            _backward = backward;
        }

        public void Backward()
        {
            _backward.Invoke();
        }

        public void Forward()
        {
            _forward.Invoke();
        }
    }
}
