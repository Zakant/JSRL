using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Logging
{
    public static class LoggingHelper
    {
        public static Engine setupLogging(this Engine engine, string programName)
        {
            var logger = Logger.Create(programName);
            engine.SetValue("Logger", logger);
            engine.SetValue("log", new Action<object>(logger.Log));
            return engine;
        }

        public static void CopyLogger(Engine source, Engine target)
        {
            var log = source.GetValue("log").AsObject();
            log.Engine = target;
            target.SetValue("log", log);
        }
    }
}
