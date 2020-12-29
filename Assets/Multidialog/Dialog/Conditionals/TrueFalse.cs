using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.Conditionals
{
    public class ConstantTrue : DialogConditional
    {
        public override string Name => "true";
        public override Syntax Syntax => Syntax.Empty;

        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
        {
            output.Result = true;
            yield break;
        }
    }

    public class ConstantFalse : DialogConditional
    {
        public override string Name => "false";
        public override Syntax Syntax => Syntax.Empty;

        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
        {
            output.Result = false;
            yield break;
        }
    }
}
