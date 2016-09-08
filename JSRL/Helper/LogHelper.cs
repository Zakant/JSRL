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
                StringBuilder sb = new StringBuilder();
                if (!String.IsNullOrWhiteSpace(prefix))
                {
                    sb.Append(prefix);
                    sb.Append("_");
                }
                sb.Append(programName);
                sb.Append("_");
                if (!String.IsNullOrWhiteSpace(suffix))
                {
                    sb.Append(suffix);
                    sb.Append("_");
                }
                sb.Append(i);
                sb.Append(".log");

                string testPath = Path.Combine(folder, sb.ToString());
                if (!File.Exists(testPath)) return testPath;
                i++;
            }
        }
    }
}
