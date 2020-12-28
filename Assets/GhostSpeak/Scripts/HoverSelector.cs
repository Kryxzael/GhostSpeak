using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSelector : MonoBehaviour
{
    public Camera toolCamera;
    public Canvas myCanvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }
}
