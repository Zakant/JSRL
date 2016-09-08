using Jint;
using JSRL.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.Logging
{
    public class Logger : IDisposable
    {
        const string LogPath = "/home/root/jsrl/logs";

        public string Name { get; }
        public DateTime StartTime { get; } = DateTime.UtcNow;

        private FileStream _fs;
        private StreamWriter _w;

        private Logger(string name)
        {
            Name = name;
            DateTime now = DateTime.UtcNow;

            string path = LogHelper.getLogPath(LogPath, name);
            _fs = new FileStream(path, FileMode.Create);
            _w = new StreamWriter(_fs);
        }
        static Logger()
        {
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
        }

        public static Logger Create(string programmName)
        {
            return new Logger(programmName);
        }

        ~Logger()
        {
            Dispose();
        }

        public void Dispose()
        {
            _w.Flush();
            _w.Close();
            _w.Dispose();
            _fs.Dispose();
        }

        public void Log(object o)
        {
            _w.WriteLine(o);
            _w.Flush();
        }
    }
}
