using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dialog.Internals.Instructions
{
    public class ShowOptionsBoxInstruction : DialogInstruction
    {
        public override IEnumerator Execute(DialogSystem target)
        {
            target.OptionsBox.OpenOptionsBox();

            while (target.OptionsBox.IsOpen) yield return new WaitForEndOfFrame();

            string newId = target.OptionsBox.SelectedOption.TargetDialogIdentifier;
            if (!string.IsNullOrWhiteSpace(newId))
            {
                target.ResetDialogAndOptions();
                target.StartDialog(newId);
            }

        }
    }


}

