using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class GhostBob : MonoBehaviour
{
    private float _initialY;

    public float BobHeight = 0.25f;
    public float BobRate = 0.25f;

    private void Awake()
    {
        _initialY = transform.position.y;
    }

    private void Update()
    {
        float y = _initialY + (Mathf.Sin(BobRate * Time.time) * BobHeight);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}