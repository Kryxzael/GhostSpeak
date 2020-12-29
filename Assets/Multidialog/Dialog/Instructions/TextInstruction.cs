using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to append text to the text crawler's buffer
    /// </summary>
    public class TextInstruction : DialogInstruction
    {
        public string Text { get; }

        public TextInstruction(string text)
        {
            Text = text;
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.TextLabel.GetComponent<CrawlingText>().Text += Text + Environment.NewLine;
            yield break;
        }
    }

}
