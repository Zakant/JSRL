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
            return JsrlProgramManager.Instance.getPrograms().Select(x => (IChildItem)new JsrlProgramItem(x)).ToList();
        }
    }
}
