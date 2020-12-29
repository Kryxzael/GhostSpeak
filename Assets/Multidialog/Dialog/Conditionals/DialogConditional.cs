using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog
{
    /// <summary>
    /// Represents a dialog conditional expression. Which can be used in ?? and !? expressions. Please note that these instructions cannot be run normaly
    /// </summary>
    public abstract class DialogConditional : CustomInstruction
    {
        //Final override of Execute, Conditionals cannot be executed as custom instruction, despite being so
        public sealed override IEnumerator Execute(DialogSystem target, Params args)
            => throw new Exception(string.Format("{0} is a conditional, and cannot be executed as an instruction", Name));

        /// <summary>
        /// Evaluates the conditional, and stores the output in the ConditionalResult instance
        /// </summary>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public abstract IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output);


        /// <summary>
        /// Stores the output of a conditional instruction
        /// </summary>
        public class ConditionalResult
        {
            /*
             * This is essentialy a boolean wrapper
             * This is necessary because coroutines cannot return values, nor have out params
             */

            /// <summary>
            /// The result of the conditional
            /// </summary>
            public bool Result { get; set; }

            public ConditionalResult(bool result)
            {
                Result = result;
            }
        }
    }
}
