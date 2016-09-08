using JSRL.Robotics.Debugger;
using JSRL.Robotics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonoBrickFirmware.FileSystem
{
    public class JsrlProgramManager
    {
        public const string JsrlPath = "/home/root/jsrl";
        public const string logPath = "/home/root/jsrl/logs";
        public const string includePath = "/home/root/jsrl/includes";

        public static JsrlProgramManager Instance { get; } = new JsrlProgramManager();

        public void CreateProgramFolder()
        {
            if (!Directory.Exists(JsrlPath))
                Directory.CreateDirectory(JsrlPath);
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            if (!Directory.Exists(includePath))
                Directory.CreateDirectory(includePath);
        }

        public List<JsrlProgram> getPrograms()
        {
            return Directory.EnumerateFiles(JsrlPath, "*.js").Select(x =>
            {
                int indexSlash = x.LastIndexOf('/') + 1;
                int indexDot = x.LastIndexOf('.');
                return new JsrlProgram(x.Substring(indexSlash, indexDot - indexSlash), x);
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
            try
            {
                string code = File.ReadAllText(program.Path);
                if (JsrlDebugger.Instance.isAttached)
                    JsrlDebugger.Instance.prepareEngine(engine);
                engine.Execute(code); // Running the program here
            }
            catch (Exception ex)
            {
                exception = ex;
                JsrlDebugger.Instance.SendException(ex);
                LogException(program, ex);

            }
            finally
            {
                engine.Destroy();
                onDone(exception);
            }
        }

        public void LogException(JsrlProgram program, Exception ex)
        {
            string target = Path.Combine(logPath + $"{program.Name}.log");
            File.WriteAllText(target, $"{ex.ToString()}{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
        }
    }
}
