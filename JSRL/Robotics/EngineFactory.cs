using JSRL.Robotics.Motors;
using JSRL.Robotics.Sensors;
using JSRL.Robotics.Threading;
using Jint;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics
{
    public static class EngineFactory
    {
        private static Dictionary<string, object> _rootElements = new Dictionary<string, object>();

        static EngineFactory()
        {
            foreach (var entry in new[] { SensorPort.In1, SensorPort.In2, SensorPort.In3, SensorPort.In4 }) // Sensor objects
                _rootElements.Add(getName(entry), WrapperFactory.Instance.CreateWrapper(SensorFactory.GetSensor(entry)));
            // All motors
            _rootElements.Add("MotorA", new MotorWrapper(MotorPort.OutA));
            _rootElements.Add("MotorB", new MotorWrapper(MotorPort.OutB));
            _rootElements.Add("MotorC", new MotorWrapper(MotorPort.OutC));
            _rootElements.Add("MotorD", new MotorWrapper(MotorPort.OutD));

            _rootElements.Add("Delay", new Action<int>(HelperFunctions.Wait));

            // Sensor modes
            _rootElements.Add("Mode", new
            {
                ColorMode = new { Ambient = ColorMode.Ambient, Color = ColorMode.Color, Reflection = ColorMode.Reflection, RGB = ColorMode.RGB },
                GyroMode = new { Angle = GyroMode.Angle, AngularVelocity = GyroMode.AngularVelocity }
            });

            _rootElements.Add("Display", new DisplayWrapper());
        }

        public static Engine CreateEngine()
        {
            var engine = new Engine(cfg => cfg.AllowClr(typeof(Motor).Assembly));

            foreach (var x in _rootElements)
                engine.SetValue(x.Key, x.Value);

            engine.setupThreading().setupIPC();

            return engine;
        }

        private static string getName(SensorPort port)
        {
            int id = (int)port;
            return $"Sensor{id + 1}";
        }
    }
}
