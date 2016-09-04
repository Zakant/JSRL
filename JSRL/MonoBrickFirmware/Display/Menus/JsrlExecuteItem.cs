using MonoBrickFirmware.Display;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoBrickFirmware.Display.Menus
{
    internal class JsrlExecuteItem : ChildItem
    {
        JsrlProgram Program { get; }

        private ItemWithDialog<InfoDialog> _exceptionDialog = new ItemWithDialog<InfoDialog>(new InfoDialog("An exception occured! See logs for more details!", "Exception"));

        public JsrlExecuteItem(JsrlProgram program)
        {
            Program = program;
        }

        public void Start(IParentItem parent)
        {
            Parent = parent;
            Parent.SetFocus(this);
            StartProgram();
        }

        private void StartProgram()
        {
            Parent.SuspendButtonEvents();
            Lcd.Clear();
            Lcd.Update();
            JsrlProgramManager.Instance.RunProgram(Program, OnDone);
        }

        private void OnDone(Exception ex)
        {
            Parent.ResumeButtonEvents();

            if (ex != null)
            {
                _exceptionDialog.SetFocus(Parent);
            }
            else
            {
                Parent.RemoveFocus(this);
            }
        }
    }
}
