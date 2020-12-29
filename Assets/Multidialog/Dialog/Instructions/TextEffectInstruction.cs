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
    /// Instructs a dialog system to change the text-crawler's text effect. This is an instant instruction
    /// </summary>
    public class TextEffectInstruction : DialogInstruction
    {
        public CrawlingText.TextEffectMode EffectType;

        public TextEffectInstruction(string effectType)
        {
            switch (effectType.ToLower())
            {
                case "normal":
                    EffectType = CrawlingText.TextEffectMode.None;
                    break;
                case "shake":
                    EffectType = CrawlingText.TextEffectMode.Shake;
                    break;
                case "twitch":
                    EffectType = CrawlingText.TextEffectMode.Twitch;
                    break;
                case "wave":
                    EffectType = CrawlingText.TextEffectMode.Rotate;
                    break;
                default:
                    EffectType = CrawlingText.TextEffectMode.None;
                    Debug.LogError("Invalid text effect instruction: '" + effectType + "'. Valid options are: 'normal', 'shake', 'twitch' and 'wave'");
                    break;
            }
        }

        public override IEnumerator Execute(DialogSystem target)
        {
            target.TextLabel.GetComponent<CrawlingText>().Effects = EffectType;
            yield break;
        }
    }

}