using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.Conditionals
{
    public class RNG : DialogConditional
    {
        public override string Name => "rng";

        public override Syntax Syntax => Syntax.Begin().Add("Percent chance", Range.From(0).To(100), false);

        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
        {
            output.Result = UnityEngine.Random.Range(0, 100) <= args.ToInt(0);
            yield break;
        }
    }
}
