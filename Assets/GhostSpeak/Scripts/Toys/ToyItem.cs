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
        Label.text = Name;
    }

    public void OnClick()
    {
        Ghost ghost = FindObjectOfType<Ghost>();

        ghost.ThisIsTextBox.SetContent(ghost.Alphabet.ToBooString(Name));
    }
}
