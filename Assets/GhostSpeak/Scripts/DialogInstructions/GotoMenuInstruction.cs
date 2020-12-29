using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GotoMenuInstruction : CustomInstruction
{
    public override string Name { get; } = "gotomenu";
    public override Syntax Syntax { get; } = Syntax.Begin();

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        throw new NotImplementedException("Pretend that you are now at the main menu");
    }
}
