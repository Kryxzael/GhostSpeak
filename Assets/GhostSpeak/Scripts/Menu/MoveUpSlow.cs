using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveUpSlow : MonoBehaviour
{
    private RectTransform parentCanvas;
    public float Speed;

    private void Awake()
    {
        parentCanvas = gameObject.GetComponentInParent<Canvas>().transform as RectTransform;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * parentCanvas.sizeDelta.y);
    }
}
