using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class DumpBooAlphabet : MonoBehaviour
{
    public string TranslationInput;

    public bool InputIsBoo;

    private void Start()
    {
        BooAlphabet alpha = new BooAlphabet();

        string output = "";
        foreach (var i in alpha.Mappings)
        {
            output += i.LatinLetter + ": " + i.BooLetter + Environment.NewLine;
        }
        Debug.Log(output);

        if (!string.IsNullOrWhiteSpace(TranslationInput))
        {
            if (InputIsBoo)
                Debug.Log(alpha.ToLatinString(TranslationInput));

            else
                Debug.Log(alpha.ToBooString(TranslationInput));
        }

    }
}
