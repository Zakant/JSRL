using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoBrickFirmware.Display.Menus
{
    public class ItemWithJsrlProgramList : ItemList
    {
        public ItemWithJsrlProgramList() : base("Programs", Font.MediumFont, true)
        {
        }

        protected override List<IChildItem> OnCreateChildList()
        {
            var programs = JsrlProgramManager.Instance.getPrograms().ToList();
            if (programs.Count == 0)
            {
                new InfoDialog("No programms found!").Show();
                this.RemoveFocus(this);
                return new List<IChildItem>(); // Create an empty list and return it.
            }
            
            return programs.Select(x => (IChildItem)new JsrlProgramItem(x)).ToList();
        }
        
    }
}
