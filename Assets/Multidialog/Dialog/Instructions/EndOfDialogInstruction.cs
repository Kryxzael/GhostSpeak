using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to show the buffered textbox and thereafter end the dialog sequence.
    /// </summary>
    public class EndOfDialogInstruction : DialogInstruction
    {
        public override IEnumerator Execute(DialogSystem target)
        {
            target.ResetDialogAndOptions();
            yield break;
        }
    }

}
