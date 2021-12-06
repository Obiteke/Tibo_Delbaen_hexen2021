using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hexen.PositionSystem
{
    public class Position
    {
        public event EventHandler Activated;
        public event EventHandler Deactivated;

        public void Activate()
        {
            OnActivate(EventArgs.Empty);
        }

        public void Deactivate()
        {
            OnDeactivate(EventArgs.Empty);
        }

        protected virtual void OnActivate(EventArgs eventArgs)
        {
            var handler = Activated;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnDeactivate(EventArgs eventArgs)
        {
            var handler = Deactivated;
            handler?.Invoke(this, eventArgs);
        }
    }
}
