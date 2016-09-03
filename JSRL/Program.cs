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

            string program2 = @"
                MotorA.On(-100);
                MotorB.On(-100);
                Delay(1000);
                MotorA.Off();
                MotorB.Off();
            ";

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            var engine = EngineFactory.CreateEngine();
            sw1.Stop();
            Console.WriteLine($"Creation: {sw1.ElapsedMilliseconds}");
            sw1.Reset();
            sw1.Start();
            engine.Execute(program);
            sw1.Stop();
            Console.WriteLine($"Execution: {sw1.ElapsedMilliseconds}");

            sw1.Reset();
            sw1.Start();
            var engine2 = EngineFactory.CreateEngine();
            sw1.Stop();
            Console.WriteLine($"Creation: {sw1.ElapsedMilliseconds}");
            sw1.Reset();
            sw1.Start();
            engine2.Execute(program2);
            sw1.Stop();
            Console.WriteLine($"Execution: {sw1.ElapsedMilliseconds}");
        }
    }
}
