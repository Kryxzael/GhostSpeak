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
    public string DialogName;
    public TextMeshProUGUI Label;

    private void Start()
    {
        //Label.text = Name;
    }

    public void OnClick()
    {
        Ghost ghost = FindObjectOfType<Ghost>();
        if (ghost.GameState != GameState.Searching)
            return;

        ghost.GameState = GameState.Inspecting;
        FindObjectOfType<Dialog.DialogSystem>()
            .StartDialog(DialogName);
    }
}
