using Dialog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

public class InputPrompt : MonoBehaviour
{
    public TextMeshProUGUI InputLabel;

    public string CurrentInput;

    private Ghost _ghost;

    private void Awake()
    {
        _ghost = FindObjectOfType<Ghost>();
        SetCurrentInput("");
    }

    private void Update()
    {
        if (_ghost.GameState != GameState.Searching)
            return;

        for (KeyCode i = KeyCode.A; i <= KeyCode.Z; i++)
        {
            if (Input.GetKeyDown(i))
                AmendCurrentInput(i.ToString());
        }
            

        if (Input.GetKeyDown(KeyCode.Backspace) && CurrentInput.Length > 0)
            SetCurrentInput(CurrentInput.Substring(0, CurrentInput.Length - 1));

        else if (Input.GetKeyDown(KeyCode.Space))
            AmendCurrentInput(" ");

        else if (Input.GetKeyDown(KeyCode.Return))
            SubmitInput();
    }

    public void SetCurrentInput(string newCurrentInput)
    {
        InputLabel.color = Color.black;
        InputLabel.text = CurrentInput = newCurrentInput;

        if (string.IsNullOrEmpty(CurrentInput))
            InputLabel.text = "(type)";
    }

    public void AmendCurrentInput(string amendedText)
    {
        SetCurrentInput(CurrentInput + amendedText);
    }

    public void SubmitInput()
    {
        Ghost ghost = FindObjectOfType<Ghost>();
        DialogSystem dialog = FindObjectOfType<DialogSystem>();
        dialog.Bank = "Outro";

        if (string.IsNullOrWhiteSpace(CurrentInput))
        {
            ghost.GameState = GameState.GuessIncorrect;
            dialog.StartDialog("blankInput");
        }
        else if (CurrentInput.Trim().Length > ghost.TargetWord.Length)
        {
            ghost.GameState = GameState.GuessIncorrect;
            dialog.StartDialog("tooLong");
        }
        else if (CurrentInput.Trim().Length < ghost.TargetWord.Length)
        {
            ghost.GameState = GameState.GuessIncorrect;
            dialog.StartDialog("tooShort");
        }
        else if (ghost.TargetWord.ToUpper() == CurrentInput.Trim())
        {
            ghost.GameState = GameState.GuessCorrect;
            dialog.StartDialog("correct");
        }
        else
        {
            ghost.GameState = GameState.GuessIncorrect;
            dialog.StartDialog("incorrect");
        }

    }
}
