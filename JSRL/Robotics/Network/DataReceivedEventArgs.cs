using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Network
{
    public class DataReceivedEventArgs : EventArgs
    {
        public dynamic Data { get; }
        public DataReceivedEventArgs(dynamic data)
        {
            Data = data;
        }
    }
}
