using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.CustomInstructions
{
    /// <summary>
    /// Logs a message to the Unity console
    /// </summary>
    public class Log : CustomInstruction
    {
        public override string Name => "log";

        public override Syntax Syntax => Syntax.Begin().AddTrailing("text");

        public override IEnumerator Execute(DialogSystem target, Params args)
        {
            UnityEngine.Debug.Log(args.JoinEnd(0));
            yield break;
        }
    }
}
