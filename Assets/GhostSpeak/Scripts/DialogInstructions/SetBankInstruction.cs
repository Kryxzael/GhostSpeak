using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SetBankInstruction : CustomInstruction
{
    public override string Name { get; } = "bank";
    public override Syntax Syntax { get; } = Syntax.Begin().Add("bankname");

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        target.Bank = args[0];
        yield break;
    }
}
