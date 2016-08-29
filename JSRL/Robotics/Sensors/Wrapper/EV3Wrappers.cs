using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics.Sensors.Wrapper
{
    [TargetType(typeof(EV3TouchSensor))]
    public class EV3TouchWrapper : GenericSensorWrapper<EV3TouchSensor>
    {
        public bool isPressed() => Sensor.IsPressed();

        public EV3TouchWrapper(EV3TouchSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(EV3ColorSensor))]
    public class EV3ColorWrapper : GenericSensorWrapper<EV3ColorSensor>
    {
        public RGBColor ReadColor() => Sensor.ReadRGB();

        public int Read() => Sensor.Read(); // Maybe Sensor.ReadRaw()

        public void setMode(ColorMode mode) => Sensor.Mode = mode;

        public EV3ColorWrapper(EV3ColorSensor sensor) : base(sensor)
        {
            sensor.Mode = ColorMode.Reflection;
        }
    }

    [TargetType(typeof(EV3GyroSensor))]
    public class EV3GyroWrapper : GenericSensorWrapper<EV3GyroSensor>
    {
        public int Read() => Sensor.Read();

        public void setMode(GyroMode mode) => Sensor.Mode = mode;

        public EV3GyroWrapper(EV3GyroSensor sensor) : base(sensor)
        {
            sensor.Mode = GyroMode.Angle;
            
        }
    }

    [TargetType(typeof(EV3UltrasonicSensor))]
    public class EV3UltrasonicWrapper : GenericSensorWrapper<EV3UltrasonicSensor>
    {

        public int Read() => Sensor.Read();

        public EV3UltrasonicWrapper(EV3UltrasonicSensor sensor) : base(sensor)
        {
            sensor.Mode = UltraSonicMode.Centimeter;
        }
    }
}
