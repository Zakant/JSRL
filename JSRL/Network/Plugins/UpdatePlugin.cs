using JSRL.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace JSRL.Network.Plugins
{
    public class UpdatePlugin : NetworkPlugin
    {
        const string MONOPATH = "";

        private static string UpdatePath = Path.Combine(Environment.CurrentDirectory, "update");
        private static string UpdaterPath = Path.Combine(Environment.CurrentDirectory, "updater.exe");


        public UpdatePlugin()
        {
            TargetNames.Add("UpdatePackage");
            TargetNames.Add("UpdateUpdater");
            TargetNames.Add("UpdateComplete");
        }

        public override void HandleMessage(string TargetName, dynamic message)
        {
            switch (TargetName)
            {
                case "UpdatePackage": // We received a new update package. Save it to the update folder
                    if (Directory.Exists(UpdatePath)) Directory.Delete(UpdatePath, true);
                    Directory.CreateDirectory(UpdatePath);
                    foreach (var entry in message.Item)
                        File.WriteAllBytes(Path.Combine(UpdatePath, entry.Path), entry.Data);
                    break;
                case "UpdateUpdater": // We received the updater that deploys the update
                    if (File.Exists(UpdaterPath)) File.Delete(UpdaterPath);
                    File.WriteAllBytes(UpdaterPath, message.Updater);
                    break;
                case "UpdateComplete": // Everything was received. Start the update

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = MONOPATH;
                    psi.Arguments = UpdaterPath;
                    psi.UseShellExecute = false;

                    Process.Start(psi);

                    GlobalEvents.RaiseApplicationShutdown(); // Inform all others that the application now exits!
                    Environment.Exit(0); // Stop JSRL from running
                    break;
            }
        }
    }
}
