using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonoBrickFirmware.Sensors
{
    public class SensorListenerExt : IDisposable
    {
        private SensorDetector detector = new SensorDetector();
        private ManualResetEvent terminate = new ManualResetEvent(false);
        private bool run = false;
        Thread thread = null;
        private int interval = 0;
        public event Action<ISensor> SensorAttached
        {
            add { detector.SensorAttached += value; }
            remove { detector.SensorAttached -= value; }
        }
        public event Action<SensorPort> SensorDetached
        {
            add { detector.SensorDetached += value; }
            remove { detector.SensorDetached -= value; }
        }

        public SensorListenerExt() : this(1000)
        {
        }

        public SensorListenerExt(int interval)
        {
            this.interval = interval;
            thread = new Thread(ListenThread);
            terminate.Reset();
            run = true;
            thread.Start();
        }

        private void ListenThread()
        {
            while (run)
            {
                detector.Update();
                terminate.WaitOne(interval);
            }
        }

        public void Dispose()
        {
            run = false;
            terminate.Set();
            thread.Join();
        }
    }
}
