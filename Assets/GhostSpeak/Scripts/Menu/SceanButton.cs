using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceanButton : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("VideoScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
