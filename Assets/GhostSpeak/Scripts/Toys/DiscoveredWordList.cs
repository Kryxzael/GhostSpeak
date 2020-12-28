using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

public class DiscoveredWordList : MonoBehaviour
{
    private Dictionary<string, string> EnglishToBoo { get; } = new Dictionary<string, string>();

    public TextMeshProUGUI Label;

    private void Awake()
    {
        Label.text = "";
    }

    public void AddWord(string english, string boo)
    {
        EnglishToBoo[english] = boo;
        Refresh();
    }

    private void Refresh()
    {
        Label.text = string.Join(
            separator: Environment.NewLine, 
            values: EnglishToBoo
                .OrderBy(i => i.Key)
                .Select(i => "• " + i.Key + ": " + i.Value)
        );
    }
}
