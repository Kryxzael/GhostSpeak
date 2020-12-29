using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to jump to another label and begin executing from there. This is an instant instruction
    /// </summary>
    public class GotoInstruction : DialogInstruction
    {
        public string LabelName { get; }

        public GotoInstruction(string labelName)
        {
            LabelName = labelName.Trim();
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.StartDialog(LabelName);
            yield break;
        }
    }
}
