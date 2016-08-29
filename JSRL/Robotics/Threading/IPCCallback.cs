using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Threading
{
    public class IPCCallback
    {
        public string ChannelName { get; protected set; }

        private static object _idLock = new object();
        private static int _id = 0;
        public int ID { get; protected set; }

        public JsValue Callback { get; protected set; }

        public IPCCallback(string channelName, JsValue callback)
        {
            if (String.IsNullOrWhiteSpace(channelName)) throw new ArgumentException($"{nameof(channelName)} is not a valid channel name!");
            if (!(callback.IsObject() && callback.AsObject().Class == "function")) throw new ArgumentException($"{nameof(callback)} is not a function!");
            ChannelName = channelName.ToLower();
            Callback = callback;
            lock(_idLock)
                ID = _id++;
        }
    }
}
