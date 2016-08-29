using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics.Sensors.Wrapper
{
    public abstract class GenericSensorWrapper<T> : ISensorWrapper where T : ISensor
    {
        public T Sensor { get; protected set; }

        public GenericSensorWrapper(T sensor)
        {
            Sensor = sensor;
        }

        public T getSensor() => Sensor;
    }
}
