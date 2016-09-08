using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JSRL.Helper
{
    public static class LogHelper
    {
        public static string getLogPath(string folder, string programName, string prefix = "", string suffix = "")
        {
            int i = 0;
            while (true)
            {
                string testPath = Path.Combine(folder, $"{suffix}_{programName}_{suffix}_{i}.log");
                if (!File.Exists(testPath)) return testPath;
                i++;
            }
        }
    }
}
