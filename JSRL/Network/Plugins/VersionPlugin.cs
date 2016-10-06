using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Network.Plugins
{
    public class VersionPlugin : NetworkPlugin
    {
        public VersionPlugin()
        {
            TargetNames.Add("Version");
        }
        public override void HandleMessage(dynamic message)
        {
            message.Version = this.GetType().Assembly.GetName().Version;
            NetworkService.Instance.Send(message);
        }
    }
}
