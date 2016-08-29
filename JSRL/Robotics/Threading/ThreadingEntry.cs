using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JSRL.Robotics.Threading
{
    public class ThreadingEntry
    {

        public Engine Engine { get; protected set; }

        public Thread Thread { get; protected set; }

        private static object _lock = new object();
        private static int _id = 0;
        public int ID { get; protected set; }

        public ThreadingEntry(Engine engine, Thread thread)
        {
            Engine = engine;
            Thread = thread;
            lock (_lock)
                ID = _id++;
        }
    }
}
