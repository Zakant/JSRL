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
        protected Motor _motor;

        public void On(int speed) => _motor.SetSpeed((sbyte)speed);

        public void Off() => _motor.Off();

        public void Brake() => _motor.Brake();

        public Motor GetMotor()
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
