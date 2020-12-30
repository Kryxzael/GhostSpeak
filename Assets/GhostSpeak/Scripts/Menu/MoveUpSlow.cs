using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveUpSlow : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector2.up * Speed);
    }
}
