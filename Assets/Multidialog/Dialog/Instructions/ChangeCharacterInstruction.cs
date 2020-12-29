using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog.Internals.Instructions
{
    /// <summary>
    /// Instructs a dialog system to change its active character. This is an instant instruction
    /// </summary>
    public class ChangeCharacterInstruction : DialogInstruction
    {
        public string CharacterIdentifier { get; }

        public ChangeCharacterInstruction(string charid)
        {
            CharacterIdentifier = charid;
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.ChangeActiveCharacter(CharacterIdentifier);
            yield break;
        }
    }

}
