using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Empty instruction
    /// </summary>
    public class NopInstruction : DialogInstruction
    {
        public override IEnumerator Execute(DialogSystem target)
        {
            yield break;
        }
    }
}

