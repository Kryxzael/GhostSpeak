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

    public GameState GameState;

    private void Awake()
    {
        FindObjectOfType<DialogSystem>().CustomInstructions.AddRange(new[] {
            new FaceInstruction("normal", i => i.SetNormal()),
            new FaceInstruction("happy", i => i.SetHappy()),
            new FaceInstruction("angry", i => i.SetAngry()),
            new FaceInstruction("sad", i => i.SetSad()),
            new FaceInstruction("tired", i => i.SetTired()),
            new FaceInstruction("surprised", i => i.SetSurprised()),
        });

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
