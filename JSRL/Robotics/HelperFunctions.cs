using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSRL.Robotics
{
    public static class HelperFunctions
    {
        public static void Wait(int delay)
        {
            Thread.Sleep(delay);
        }
    }
}
