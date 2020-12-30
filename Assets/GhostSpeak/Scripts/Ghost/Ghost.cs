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
    public static readonly string[] AVAILABLE_WORDS = new[]
    {
        "NUMBER",
        "PEOPLE",
        "BEFORE",
        "RACING",
        "ACORNS",
        "QUICK",
        "JUMBO",
        "ADMIT",
        "CAMERA",
        "DECADE",
        "FLYING",
        "NATURE",
        "PLANET",
        "CABLE",
        "EMPTY",
        "HORSE",
        "NURSE",
        "RADIO",
        "SMILE",
        "THICK",
        "UNION",
        "TRUTH",
        "YOUNG",
        "RIVAL",
        "PRIZE",
        "NOISE",
        "MOVIE",
        "RANDOM",
        "PHRASE",
        "ORGIN",
        "MEDIUM",
        "MARKET",
        "GROUND",
        "FACTOR",
        "ESCAPE",
        "DOCTOR",
        "CREDIT",
        "BUDGET",
        "AVENUE",
        "ADVISE",
        "ACTION",
        "BEAUTY",
        "MOUNT",
        "PAINT",
        "CENTER",
        "COUNTY",
        "PLANT",
        "REACH",
        "PRIME",
        "SCALE",
        "SINCE",
        "LIQUID",
        "SUGAR",
        "FACING",
        "WAXING",
        "FRENCH",
        "WEIGHT",
        "MATURE",
    }.Where(i => i.All(i => new string[] { "DOLL", "BIRD", "KIWI", "ZEBRA", "JET", "VASE", "PHONE", "SUN", "TEDDY" }.SelectMany(i => i).Contains(i))).ToArray();



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

        TriggerInitialDialog();
    }

    public void ResetGame()
    {
        TargetWord = AVAILABLE_WORDS[UnityEngine.Random.Range(0, AVAILABLE_WORDS.Length)];
        Alphabet = new BooAlphabet();

        FindObjectOfType<InputPrompt>().SetCurrentInput("");
        IWantTextBox.ShowBox(Alphabet.ToBooString(TargetWord) + "?", float.PositiveInfinity);
    }

    public void TriggerInitialDialog()
    {
        DialogSystem system = FindObjectOfType<DialogSystem>();
        system.StartDialog("intro");
    }
}
