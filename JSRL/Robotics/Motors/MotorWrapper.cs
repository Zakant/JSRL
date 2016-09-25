using MonoBrickFirmware.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics.Motors
{
    public class MotorWrapper
    {
        private object _lock = new object();

        protected Motor _motor;

        public void On(int speed)
        {
            lock (_lock)
                if (speed == 0) _motor.Brake();
                else if (_motor.GetSpeed() != speed)
                    _motor.SetSpeed((sbyte)speed);
        }

        public void Off() => _motor.Off();

        public void Brake() => _motor.Brake();

        public Motor getMotor()
        {
            return _motor;
        }

        public MotorWrapper(MotorPort port)
        {
            _motor = new Motor(port);
        }

        ~MotorWrapper()
        {
            Off();
        }
    }
}
