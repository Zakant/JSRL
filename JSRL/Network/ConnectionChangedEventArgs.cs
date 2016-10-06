using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Network
{
    public class ConnectionChangedEventArgs : EventArgs
    {
        public ConnectionStatus OldStatus { get; protected set; }
        public ConnectionStatus NewStatus { get; protected set; }

        public ConnectionChangedEventArgs(ConnectionStatus oldStatus, ConnectionStatus newStatus)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }

    }
}
