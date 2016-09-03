using JSRL.Robotics.Sensors.Wrapper;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics.Sensors
{
    public class WrapperFactory
    {
        private static WrapperFactory _instance = null;
        public static WrapperFactory Instance
        {
            get { return _instance ?? (_instance = new WrapperFactory()); }
        }

        private Dictionary<Type, Func<ISensor, ISensorWrapper>> _factories = new Dictionary<Type, Func<ISensor, ISensorWrapper>>();


        public WrapperFactory()
        {
            Assembly ass = this.GetType().Assembly;
            var types = ass.GetTypes();
            foreach (var t in types)
            {
                var attributes = t.GetCustomAttributes(typeof(TargetTypeAttribute), false);

                if (attributes.Length > 0)
                {
                    var attribute = (TargetTypeAttribute)attributes[0];
                    if (attribute != null)
                        _factories.Add(attribute.Target, (sensor) => (ISensorWrapper)Activator.CreateInstance(t, new[] { sensor }));
                }
            }
        }

        public ISensorWrapper CreateWrapper(ISensor sensor)
        {
            if (sensor == null || sensor is NoSensor) return null;
            return _factories[sensor.GetType()].Invoke(sensor);
        }
    }
}
