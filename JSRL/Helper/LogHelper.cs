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
            if (!String.IsNullOrWhiteSpace(prefix)) prefix = prefix + "_";
            if (!String.IsNullOrWhiteSpace(suffix)) suffix = "_" + suffix;
            int i = 0;
            while (true)
            {
                string testPath = $"{prefix}{programName}{suffix}_{i}.log";
                if (!File.Exists(testPath)) return testPath;
                i++;
            }
        }
    }
}
