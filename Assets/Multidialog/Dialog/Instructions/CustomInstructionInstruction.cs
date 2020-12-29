using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to run a custom instruction. This is an instant instruction
    /// </summary>
    public class CustomInstructionInstruction : DialogInstruction
    {
        /// <summary>
        /// The name of the custom instruction to execute
        /// </summary>
        public string Instruction { get; }

        /// <summary>
        /// The arguments passed to the custom instruction
        /// </summary>
        public string[] Args { get; }

        public CustomInstructionInstruction(string raw)
        {
            Instruction = raw.Split(' ')
                .First()
                .ToLower();

            Args = raw.Split(' ')
                .Skip(1)
                .ToArray();
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            yield return target.RunCustomInstructionAsync(Instruction, new Params(Args));
        }
    }

}

