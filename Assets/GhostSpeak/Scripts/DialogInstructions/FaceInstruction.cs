using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FaceInstruction : CustomInstruction
{
    public override string Name { get; }
    public override Syntax Syntax { get; } = Syntax.Begin();

    private Action<GhostFaceSelector> OnExecute { get; }

    public FaceInstruction(string name, Action<GhostFaceSelector> onExecute)
    {
        Name = name;
        OnExecute = onExecute;
    }

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        OnExecute(UnityEngine.Object.FindObjectOfType<GhostFaceSelector>());
        yield break;
    }
}
