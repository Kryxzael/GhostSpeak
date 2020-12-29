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
    /// Instructs a dialog system to process an instruction that has a conditional clause
    /// </summary>
    public class ConditionalInstructionInstruction : DialogInstruction
    {
        /// <summary>
        /// The dialog condition to evalutate
        /// </summary>
        public string Conditional { get; }

        /// <summary>
        /// The instruction to potentially parse, based on the conditional
        /// </summary>
        public string ConditionalInstruction { get; }

        /// <summary>
        /// Will the conditional check be inverted
        /// </summary>
        public bool Inverse { get; }

        public ConditionalInstructionInstruction(string conditionalStatement, string conditionalInstruction, bool inverse)
        {
            Conditional = conditionalStatement.Trim();
            ConditionalInstruction = conditionalInstruction.Trim();
            Inverse = inverse;
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            //We are already skipping scopes
            if (target.SkippingScopes > 0)
            {
                if (OpensScope())
                {
                    target.SkippingScopes++;
                }

                yield break;
            }


            //Splits the conditional by spaces to form a command and argument portions
            string[] splitBySpaces = Conditional.Split(' ');

            //Creates output device and evalutates the conditional
            DialogConditional.ConditionalResult result = new DialogConditional.ConditionalResult(false);
            yield return target.EvalutateConditionalAsync(splitBySpaces[0], new Params(splitBySpaces.Skip(1)), result);

            //Runs the instruction if the condition is met
            if (result.Result == !Inverse)
            {
                //Scope blocks, do not do anything (Let the next lines execute)
                if (OpensScope())
                {
                    yield break;
                }

                yield return DialogBankParser.ParseLine(ConditionalInstruction).Execute(target);
            }

            //Scope blocks, skip next block
            else if (OpensScope())
            {
                target.SkippingScopes++;
            }
        }

        private bool OpensScope()
        {
            return ConditionalInstruction.Trim() == "{";
        }
    }
}

