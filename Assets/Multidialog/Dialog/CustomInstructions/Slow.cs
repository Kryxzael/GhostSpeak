using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.CustomInstructions
{
    public class Slow : CustomInstruction
    {
        public override string Name => "slow";
        public override Syntax Syntax => Syntax.Empty;

        public override IEnumerator Execute(DialogSystem target, Params args)
        {
            target.TextLabel.GetComponent<CrawlingText>().Slowdown = true;
            yield break;
        }
    }
}
