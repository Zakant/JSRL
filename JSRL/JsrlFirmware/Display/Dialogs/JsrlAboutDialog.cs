using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.UserInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsrlFirmware.Display.Dialogs
{
    public class JsrlAboutDialog : InfoDialog
    {
        public JsrlAboutDialog() : base("", "About")
        {
        }

        protected override void OnDrawContent()
        {
            this.WriteTitle();
            this.WriteTextOnLine($"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}", 0);
            this.WriteTextOnLine("Creator: Christian Wieck", 1);
            this.DrawCenterButton("Ok", true);
        }
    }
}
