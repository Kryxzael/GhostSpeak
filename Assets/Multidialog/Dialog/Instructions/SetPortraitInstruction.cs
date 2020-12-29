using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to set the character portrait. This is an instant instruction
    /// </summary>
    public class SetPortraitInstruction : DialogInstruction
    {
        public string PortraitIdentifier { get; }

        public SetPortraitInstruction(string portraitId)
        {
            PortraitIdentifier = portraitId;
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.SetActivePortrait(PortraitIdentifier);
            yield break;
        }
    }

}

