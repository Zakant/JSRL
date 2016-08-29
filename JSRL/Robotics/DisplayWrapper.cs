using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics
{
    public class DisplayWrapper
    {
        
        public void WriteLine(string text) => LcdConsole.WriteLine(text);
        public DisplayWrapper()
        {

        }
    }
}
