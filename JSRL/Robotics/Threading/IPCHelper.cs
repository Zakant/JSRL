using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Threading
{
    public static class IPCHelper
    {
        private static List<IPCCallback> _channelCallbacks = new List<IPCCallback>();

        public static int RegisterIPC(string channelName, JsValue callback)
        {
            var c = new IPCCallback(channelName, callback);
            _channelCallbacks.Add(c);
            return c.ID;
        }

        public static void RemoveIPC(int id)
        {
            _channelCallbacks.RemoveAll(x => x.ID == id);
        }

        public static void SendIPC(string channelName, JsValue data)
        {
            var name = channelName.ToLower();
            var targets = _channelCallbacks.Where(x => x.ChannelName == name);
            foreach (var t in targets)
                t.Callback.Invoke(data);
        }

        public static Engine setupIPC(this Engine engine)
        {
            engine.SetValue("ipc", new
            {
                Register = new Func<string, JsValue, int>(RegisterIPC),
                Remove = new Action<int>(RemoveIPC),
                Send = new Action<string, JsValue>(SendIPC)
            });
            return engine;
        }
    }
}
