using JSRL.Helper;
using JSRL.Robotics;
using JSRL.Robotics.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsrlFirmware.FileSystem
{
    public class JsrlProgramManager
    {
        public const string JsrlPath = "/home/root/jsrl";
        public const string errorPath = "/home/root/jsrl/logs";
        public const string includePath = "/home/root/jsrl/includes";

        public static JsrlProgramManager Instance { get; } = new JsrlProgramManager();

        public void CreateProgramFolder()
        {
            if (!Directory.Exists(JsrlPath))
                Directory.CreateDirectory(JsrlPath);
            if (!Directory.Exists(errorPath))
                Directory.CreateDirectory(errorPath);
            if (!Directory.Exists(includePath))
                Directory.CreateDirectory(includePath);
        }

        public List<JsrlProgram> getPrograms()
        {
            return Directory.EnumerateFiles(JsrlPath, "*.js").Select(x =>
            {
                return JsrlProgram.fromPath(x);
            }).ToList();
        }

        public bool DeleteProgrm(JsrlProgram program)
        {
            try
            {
                File.Delete(program.Path);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void RunProgram(JsrlProgram program, Action<Exception> onDone)
        {
            // Running the program here
            var engine = EngineFactory.CreateEngine();
            Exception exception = null;
            DateTime now = DateTime.UtcNow;
            try
            {
                string code = File.ReadAllText(program.Path);
                engine.setupLogging(program.Name).Execute(code); // Running the program here
            }
            catch (Exception ex)
            {
                exception = ex;
                LogException(program, ex);
                Console.WriteLine("Exception!");
                Console.WriteLine(ex);
                Console.WriteLine("See error log for more infos!");
            }
            finally
            {
                engine.Destroy();
                onDone?.Invoke(exception);
            }
        }

        public void LogException(JsrlProgram program, Exception ex)
        {
            string target = LogHelper.getLogPath(errorPath, program.Name, "error");
            File.WriteAllText(target, $"{ex.ToString()}{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
        }
    }
}
