using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// The container that contains the box
    /// </summary>
    public GameObject Container;

    /// <summary>
    /// The text that will be displayed on the header
    /// </summary>
    public string HeaderText;

    /// <summary>
    /// The text that will be displayed as content
    /// </summary>
    public string ContentText;

    /// <summary>
    /// Will this text box be hidden by default?
    /// </summary>
    public bool HideByDefault;

    private void Awake()
    {
        HeaderLabel.text = HeaderText;
        ContentLabel.text = ContentText;

        if (HideByDefault)
            Container.SetActive(false);
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

    public void ShowBox(string content, float duration = 2.5f)
    {
        ShowBox(HeaderText, content, duration);
    }

    public void ShowBox(string header, string content, float duration = 2.5f)
    {
        StopAllCoroutines();
        StartCoroutine(co());

        IEnumerator co()
        {
            Container.SetActive(true);
            SetHeader(header);
            SetContent(content);

            yield return new WaitForSeconds(duration);

            Container.SetActive(false);
        }
    }
}