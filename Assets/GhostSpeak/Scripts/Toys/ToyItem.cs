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
        if (FindObjectOfType<Ghost>().GameState != GameState.Searching)
            return;

        FindObjectOfType<Dialog.DialogSystem>()
            .StartDialog(DialogName);
    }
}
