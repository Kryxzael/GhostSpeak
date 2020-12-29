using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class SetGameStateInstruction : CustomInstruction
{
    public override string Name { get; } = "gamestate";
    public override Syntax Syntax { get; } = Syntax.Begin().Add("statename");

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        GameState state = (GameState)Enum.Parse(typeof(GameState), args[0]);
        UnityEngine.Object.FindObjectOfType<Ghost>().GameState = state;

        yield break;
    }
}
