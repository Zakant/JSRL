using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Sensors.Wrapper
{
    [TargetType(typeof(HiTecCompassSensor))]
    public class HiTecCompassWrapper : GenericSensorWrapper<HiTecCompassSensor>
    {
        public int Read() => Sensor.ReadDirection();

        public HiTecCompassWrapper(HiTecCompassSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(HiTecGyroSensor))]
    public class HiTecGyroWrapper : GenericSensorWrapper<HiTecGyroSensor>
    {
        public int Read() => Sensor.ReadAngularAcceleration();
        public int Offset
        {
            get { return Sensor.Offset; }
            set { Sensor.Offset = value; }
        }

        public HiTecGyroWrapper(HiTecGyroSensor sensor) : base(sensor)
        {
        }
    }
}
