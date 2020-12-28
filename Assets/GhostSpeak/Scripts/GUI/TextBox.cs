using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

/// <summary>
/// Implements a text box
/// </summary>
public class TextBox : MonoBehaviour
{
    /// <summary>
    /// The text mesh pro element that represents the header
    /// </summary>
    public TextMeshProUGUI HeaderLabel;

    /// <summary>
    /// The text mesh pro element that represents the content
    /// </summary>
    public TextMeshProUGUI ContentLabel;

    /// <summary>
    /// The text that will be displayed on the header
    /// </summary>
    public string HeaderText;

    /// <summary>
    /// The text that will be displayed as content
    /// </summary>
    public string ContentText;

    private void Awake()
    {
        HeaderLabel.text = HeaderText;
        ContentLabel.text = ContentText;
    }

    public void SetHeader(string text)
    {
        HeaderText = text;
        HeaderLabel.text = text;
    }

    public void SetContent(string text)
    {
        ContentText = text;
        ContentLabel.text = text;
    }
}