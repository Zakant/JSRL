using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Network.Plugins
{
    public abstract class NetworkPlugin : IDisposable
    {
        public IList<string> TargetNames { get; } = new List<string>();

        public virtual void Dispose()
        {
        }

        public abstract void HandleMessage(dynamic message);
    }
}
