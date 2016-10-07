using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display.Menus;
using MonoBrickFirmware.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsrlFirmware.Display.Menus
{
    internal class JsrlProgramItem : ChildItemWithParent
    {
        public JsrlProgram Program { get; protected set; }

        private ItemWithDialog<SelectDialog<string>> _selectDialog;
        private ItemWithDialog<QuestionDialog> _deleteQuestionDialog;


        private static string[] _selections = new string[] { "Run", "Delete", "Abort" };
        public JsrlProgramItem(JsrlProgram program) : base(program.Name)
        {
            Program = program;
            _selectDialog = new ItemWithDialog<SelectDialog<string>>(new SelectDialog<string>(_selections, program.Name, true));
            _deleteQuestionDialog = new ItemWithDialog<QuestionDialog>(new QuestionDialog("Really delete?", "Delete"));
        }

        public override void OnEnterPressed()
        {
            _selectDialog.SetFocus(this, SelectExit);
        }

        private void SelectExit(SelectDialog<string> dialog)
        {
            if (dialog.EscPressed) return;
            switch (dialog.GetSelectionIndex())
            {
                case 0: // Run
                    var execDialog = new JsrlExecuteItem(Program);
                    execDialog.Start(this);
                    break;
                case 1: // Delete
                    _deleteQuestionDialog.SetFocus(this, DeleteExit);
                    break;
                case 2: //Abort
                    this.RemoveFocus(this);
                    return;
            }
        }
        private void DeleteExit(QuestionDialog dialog)
        {
            if (dialog.IsPositiveSelected)
                JsrlProgramManager.Instance.DeleteProgrm(Program);
        }
    }
}
