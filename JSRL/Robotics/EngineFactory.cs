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
using JSRL.Robotics.UserInput;
using MonoBrickFirmware.UserInput;
using Jint.Native;

namespace JSRL.Robotics
{
    public static class EngineFactory
    {
        private static Dictionary<string, object> _rootElements = new Dictionary<string, object>();
        private static List<Engine> _engines = new List<Engine>();

        private static SensorListner _listener;
        private static ButtonEvents _buttonEvents;

        static EngineFactory()
        {
            foreach (var entry in new[] { SensorPort.In1, SensorPort.In2, SensorPort.In3, SensorPort.In4 }) // Sensor objects
                _rootElements.Add(getName(entry), WrapperFactory.Instance.CreateWrapper(SensorFactory.GetSensor(entry)));

            _listener = new SensorListner();
            _listener.SensorAttached += (sensor) =>
            {
                var instance = WrapperFactory.Instance.CreateWrapper(sensor);
                var name = getName(sensor.Port);
                _rootElements[name] = instance;
                foreach (var e in _engines)
                {
                    e.SetValue(name, instance);
                    e.GetValue("sensorAttached").Invoke(e.GetValue(name));
                }
            };

            _listener.SensorDetached += (sensor) =>
            {

                var name = getName(sensor);
                _rootElements[name] = null;
                foreach (var e in _engines)
                {
                    var old = e.GetValue(name);
                    e.SetValue(name, new JsValue());
                    e.GetValue("sensorDetached").Invoke(name, old);
                }
            };

            //Prepare the button events
            _buttonEvents = new ButtonEvents();

            // All motors
            _rootElements.Add("MotorA", new MotorWrapper(MotorPort.OutA));
            _rootElements.Add("MotorB", new MotorWrapper(MotorPort.OutB));
            _rootElements.Add("MotorC", new MotorWrapper(MotorPort.OutC));
            _rootElements.Add("MotorD", new MotorWrapper(MotorPort.OutD));

            _rootElements.Add("delay", new Action<int>(HelperFunctions.Wait));

            // Sensor modes
            _rootElements.Add("Mode", new
            {
                Color = new { Ambient = ColorMode.Ambient, Color = ColorMode.Color, Reflection = ColorMode.Reflection, RGB = ColorMode.RGB },
                Gyro = new { Angle = GyroMode.Angle, AngularVelocity = GyroMode.AngularVelocity }
            });

            _rootElements.Add("Display", new DisplayWrapper());
        }

        public static Engine CreateEngine()
        {
            var engine = new Engine(cfg => cfg.AllowClr(typeof(Motor).Assembly).Strict().DebugMode().AllowDebuggerStatement());
            var buttons = new UserInput.Buttons(_buttonEvents);

            foreach (var x in _rootElements)
                engine.SetValue(x.Key, x.Value);

            engine.setupThreading().setupIPC();
            engine.SetValue("Buttons", buttons);
            _engines.Add(engine);

            return engine;
        }

        public static void Shutdown()
        {
            if (_listener != null)
                _listener.Dispose();
            if (_buttonEvents != null)
                _buttonEvents.Dispose();
        }

        public static void Destroy(this Engine engine)
        {
            _engines.Remove(engine);
            var buttonsClass = engine.GetValue("Buttons").ToObject();
            ((UserInput.Buttons)buttonsClass).Dispose();
        }

        private static string getName(SensorPort port)
        {
            int id = (int)port;
            return $"Sensor{id + 1}";
        }
    }
}
