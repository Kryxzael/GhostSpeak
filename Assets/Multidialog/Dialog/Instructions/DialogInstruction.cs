using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Represents an instruction that is executable by a dialog system
    /// </summary>
    public abstract class DialogInstruction
    {
        /// <summary>
        /// Executes the instruction on the given dialog system
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract IEnumerator Execute(DialogSystem target);
    }

}
