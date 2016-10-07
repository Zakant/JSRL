using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Network.Plugins
{
    public class PluginManager : IDisposable
    {
        private Dictionary<string, List<NetworkPlugin>> _plugins = new Dictionary<string, List<NetworkPlugin>>();

        public PluginManager()
        {
            var pluginType = typeof(NetworkPlugin);
            var assembly = this.GetType().Assembly;
            foreach (var t in assembly.GetTypes().Where(x => pluginType.IsAssignableFrom(x)))
            {
                var plugin = (NetworkPlugin)Activator.CreateInstance(t);
                foreach(var name in plugin.TargetNames)
                {
                    if (!_plugins.ContainsKey(name))
                        _plugins.Add(name, new List<NetworkPlugin>());
                    _plugins[name].Add(plugin);
                }
            }

            NetworkService.Instance.DataReceived += HandleDataReceived;
        }

        private void HandleDataReceived(object sender, DataReceivedEventArgs e)
        {
            string target = e.Data.Target;
            if (_plugins.ContainsKey(target))
                foreach (var p in _plugins[target])
                    p.HandleMessage(e.Data);
        }

        public void Dispose()
        {
            foreach (var plugin in _plugins.Select(x => x.Value).SelectMany(x => x).Distinct())
                plugin.Dispose();
        }
    }
}
