using Dialog;
using Dialog.Syntaxes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BooInstruction : CustomInstruction
{
    public override string Name { get; } = "boo";
    public override Syntax Syntax { get; } = Syntax.Begin().Add("word");

    public override IEnumerator Execute(DialogSystem target, Params args)
    {
        Ghost ghost = UnityEngine.Object.FindObjectOfType<Ghost>();
        string boo = ghost.Alphabet.ToBooString(args[0]);

        target.TextLabel.GetComponent<CrawlingText>().Text += boo;
        yield break;
    }
}
