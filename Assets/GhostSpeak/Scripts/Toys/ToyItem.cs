using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

public class ToyItem : MonoBehaviour
{
    public string Name;
    public TextMeshProUGUI Label;

    private void Start()
    {
        //Label.text = Name;
    }

    public void OnClick()
    {
        Ghost ghost = FindObjectOfType<Ghost>();
        DiscoveredWordList wordList = FindObjectOfType<DiscoveredWordList>();

        string booWord = ghost.Alphabet.ToBooString(Name);
        wordList?.AddWord(Name, booWord);

        ghost.ThisIsTextBox.ShowBox("<b>" + Name + "</b>" + Environment.NewLine + booWord, float.MaxValue);
    }
}
