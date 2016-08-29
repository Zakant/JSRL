using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Sensors.Wrapper
{
    [TargetType(typeof(NXTColorSensor))]
    public class NXTColorWrapper : GenericSensorWrapper<NXTColorSensor>
    {
        public object Read() => Sensor.ReadRGBColor();

        public NXTColorWrapper(NXTColorSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(NXTLightSensor))]
    public class NXTLightWrapper : GenericSensorWrapper<NXTLightSensor>
    {
        public int Read() => Sensor.Read(); // Maybe ReadRaw

        public NXTLightWrapper(NXTLightSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(NXTSoundSensor))]
    public class NXTSoundWrapper : GenericSensorWrapper<NXTSoundSensor>
    {
        public int Read() => Sensor.Read(); // Maybe ReadRaw
        public NXTSoundWrapper(NXTSoundSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(NXTTouchSensor))]
    public class NXTTouchWrapper : GenericSensorWrapper<NXTTouchSensor>
    {
        public bool isPressed() => Sensor.IsPressed();

        public NXTTouchWrapper(NXTTouchSensor sensor) : base(sensor)
        {
        }
    }

    [TargetType(typeof(NXTUltraSonicSensor))]
    public class NXTUltraSonicWrapper : GenericSensorWrapper<NXTUltraSonicSensor>
    {
        public float Read() => Sensor.ReadDistance();

        public NXTUltraSonicWrapper(NXTUltraSonicSensor sensor) : base(sensor)
        {
        }
    }
}
