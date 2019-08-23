using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Treasury.Snowflake
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Dispose()
        {
            _action();
        }
    }
}
