using Dialog;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static readonly string[] AVAILABLE_WORDS =
    {
        "Test",
    };

    public string TargetWord;
    public BooAlphabet Alphabet;

    public TextBox IWantTextBox;
    public TextBox ThisIsTextBox;

    private void Awake()
    {
        TargetWord = AVAILABLE_WORDS[UnityEngine.Random.Range(0, AVAILABLE_WORDS.Length)];
        Alphabet = new BooAlphabet();
        IWantTextBox.SetContent(Alphabet.ToBooString(TargetWord) + "?");

        TriggerInitialDialog();
    }

    public void TriggerInitialDialog()
    {
        DialogSystem system = FindObjectOfType<DialogSystem>();
        system.StartDialog("intro");
    }
}
