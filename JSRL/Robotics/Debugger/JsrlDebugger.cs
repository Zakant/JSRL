using Jint;
using JSRL.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Debugger
{
    public class JsrlDebugger
    {
        public static JsrlDebugger Instance { get; } = new JsrlDebugger();

        public JsrlDebugger()
        {
            NetworkService.Instance.ConnectionChanged += HandleConnectionChanged;
            NetworkService.Instance.DataReceived += HandleMessage;
        }

        public bool isAttached { get; protected set; }
        
        public bool DebugEnabled { get; protected set; }

        public Engine prepareEngine(Engine engine)
        {
            return engine;
        }

        public void SendException(Exception ex)
        {
            if (NetworkService.Instance.Status == ConnectionStatus.Connected)
                NetworkService.Instance.Send(new { Target = "Debug", Type = "Exception", Exception = ex });
        }

        public void Log(string data)
        {
            if (NetworkService.Instance.Status == ConnectionStatus.Connected)
                NetworkService.Instance.Send(new { Target = "Debug", Type = "Log", Data = data });
        }

        private void HandleConnectionChanged(object sender, ConnectionChangedEventArgs args)
        {

        }

        private void HandleMessage(object sender, DataReceivedEventArgs args)
        {
            if (args.Data.Target != "Debug") return;
        }
    }
}
