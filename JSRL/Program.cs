using JSRL.Robotics;
using Jint;
using Jint.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL
{
    class Program
    {
        static void Main(string[] args)
        {
            string program = @"
                MotorA.On(100);
                MotorB.On(100);
                Delay(1000);
                MotorA.Off();
                MotorB.Off();
            ";

            var engine = EngineFactory.CreateEngine();
            engine.Execute(program);

        }
    }
}
