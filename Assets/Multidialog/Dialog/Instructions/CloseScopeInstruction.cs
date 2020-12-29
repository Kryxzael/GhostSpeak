using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    public class CloseScopeInstruction : DialogInstruction
    {
        public override IEnumerator Execute(DialogSystem target)
        {
            target.SkippingScopes = Math.Max(target.SkippingScopes - 1, 0);
            yield break;
        }
    }
}
