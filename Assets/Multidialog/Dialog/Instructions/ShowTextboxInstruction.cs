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
    /// Instructs a dialog system to show the buffered dialog box and thereafter clear it
    /// </summary>
    public class ShowTextBoxInstruction : DialogInstruction
    {
        public override IEnumerator Execute(DialogSystem target)
        {
            //Opens the textbox if it wasn't already
            target.OpenTextBox();

            //Obtain the and start the text crawler unless it is empty.
            CrawlingText crawler = target.TextLabel.GetComponent<CrawlingText>();
            crawler.BeginScroll();

            //Wait for crawler to finish
            while (crawler.Scrolling) yield return new WaitForEndOfFrame();

            //There are dialog options, show options box
            if (target.OptionsBox.ActiveOptionCount > 0)
            {
                yield return new ShowOptionsBoxInstruction().Execute(target);
            }

            //There are no dialog options, wait for a keypress, then reset
            else
            {
                while (!Input.GetButton("Submit")) yield return new WaitForEndOfFrame();
                target.ResetDialogAndOptions();
            }
        }
    }
}