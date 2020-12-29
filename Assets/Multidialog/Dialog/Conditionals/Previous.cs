using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.Conditionals
{
    public class Previous : DialogConditional
    {
        public static ConditionalResult PreviousResult { get; set; }

        public override string Name => "...";
        public override Syntax Syntax => Syntax.Empty;

        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
        {
            output.Result = PreviousResult?.Result ?? false;
            yield break;
        }
    }
}
