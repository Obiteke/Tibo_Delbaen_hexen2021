using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySystem
{
    public interface IReplayableCommand
    {
        void Forward();
        void Backward();
    }
}
