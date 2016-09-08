using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Debugger
{
    public class JsrlDebugger
    {
        public static JsrlDebugger Instance { get; } = new JsrlDebugger();

        public bool isAttached { get; protected set; }

        public void StartListener()
        {

        }

        public void StopListener()
        {

        }

        public Engine prepareEngine(Engine engine)
        {
            return engine;
        }

        public void SendException(Exception ex)
        {

        }
    }
}
