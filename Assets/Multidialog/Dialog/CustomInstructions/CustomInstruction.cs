using System.Collections;
using Dialog.Syntaxes;

namespace Dialog
{
    /// <summary>
    /// Represents an instruction that can be run as '/' commands in dialog files
    /// </summary>
    public abstract class CustomInstruction
    {
        /// <summary>
        /// The name/identifier of the instruction
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The syntax of the command
        /// </summary>
        public abstract Syntax Syntax { get; }

        /// <summary>
        /// Executes the command (with arguments)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract IEnumerator Execute(DialogSystem target, Params args);
    }
}
