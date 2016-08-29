using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JSRL.Robotics.Threading
{
    public static class ThreadingHelper
    {
        private static List<ThreadingEntry> _threads = new List<ThreadingEntry>();

        public static int StartThread(JsValue thread, JsValue args)
        {
            var newEngine = EngineFactory.CreateEngine();
            var entry = new ThreadingEntry(newEngine, new Thread(() =>
            {
                if (args.Type == Jint.Runtime.Types.None)
                    args = new JsValue(new Jint.Native.Object.ObjectInstance(newEngine));
                thread.AsObject().Engine = newEngine;
                args.AsObject().Engine = newEngine;

                newEngine.SetValue("thread", thread);

                newEngine.Invoke("thread", args, new object[] { });
            }));
            entry.Thread.Start();
            _threads.Add(entry);
            return entry.ID;
        }

        public static void StopThread(int id)
        {
            var t = _threads.FirstOrDefault(x => x.ID == id);
            if (t != null)
            {
                _threads.Remove(t);
                t.Thread.Abort();
            }
        }

        public static void StopAllThreads()
        {
            foreach (var t in _threads)
                t.Thread.Abort();
            _threads.Clear();
        }

        public static Engine setupThreading(this Engine engine)
        {
            engine.SetValue("startThread", new Func<JsValue, JsValue, int>(StartThread));
            engine.SetValue("stopThread", new Action<int>(StopThread));
            engine.SetValue("stopAllThreads", new Action(StopAllThreads));
            return engine;
        }
    }
}
