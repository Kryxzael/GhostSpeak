using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.Conditionals
{
    public class Else : DialogConditional
    {
        public override string Name => "else";
        public override Syntax Syntax => Syntax.Empty;

        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
        {
            output.Result = !Previous.PreviousResult?.Result ?? true;
            yield break;
        }
    }
}
