using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ResetInstruction : CustomInstruction
{
    public override string Name { get; } = "resetgame";
    public override Syntax Syntax { get; } = Syntax.Begin();

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        Ghost ghost = UnityEngine.Object.FindObjectOfType<Ghost>();
        ghost.ResetGame();
        yield break;
    }
}
