using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoBrickFirmware.FileSystem
{
    public class JsrlProgram
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }

        public JsrlProgram(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
