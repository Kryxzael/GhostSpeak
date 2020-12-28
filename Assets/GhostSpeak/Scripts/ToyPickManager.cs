using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyPickManager : MonoBehaviour
{
    public GameObject ghostTextBox;

        #region dont look in here 
        public void onToy1Click()
    {
        ghostTextBox.GetComponent<Text>().text = "Boo Bho Bwo";
        Debug.Log("That is toy1");
    }

    public void onToy2Click()
    {
        ghostTextBox.GetComponent<Text>().text = "Buu Buo Ooh";
        Debug.Log("That is toy2");
    }

    public void onToy3Click()
    {
        ghostTextBox.GetComponent<Text>().text = "Ooo Ouo Oee";
        Debug.Log("That is toy3");
    }

    public void onToy4Click()
    {
        ghostTextBox.GetComponent<Text>().text = "Huu Hou Hoo";
        Debug.Log("That is toy4");
    }

    public void onToy5Click()
    {
        Debug.Log("That is toy5");
    }

    public void onToy6Click()
    {
        Debug.Log("That is toy6");
    }

    public void onToy7Click()
    {
        Debug.Log("That is toy7");
    }

    public void onToy8Click()
    {
        Debug.Log("That is toy8");
    }

    public void onToy9Click()
    {
        Debug.Log("That is toy9");
    }

    public void onToy10Click()
    {
        Debug.Log("That is toy10");
    }

    public void onToy11Click()
    {
        Debug.Log("That is toy11");
    }

    public void onToy12Click()
    {
        Debug.Log("That is toy12");
    }

    public void onToy13Click()
    {
        Debug.Log("That is toy13");
    }
    #endregion
}
