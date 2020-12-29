using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    public class SetDialogOptionInstruction : DialogInstruction
    {
        public string Text { get; }
        public string TargetIdentifier { get; }

        public SetDialogOptionInstruction(string text, string targetIdentifier)
        {
            Text = text;
            TargetIdentifier = targetIdentifier;
        }

        public SetDialogOptionInstruction(string text) : this(text, null) { }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.OptionsBox.GetFirstUnusedOption()?.Initialize(Text, TargetIdentifier);
            yield break;
        }
    }
}


