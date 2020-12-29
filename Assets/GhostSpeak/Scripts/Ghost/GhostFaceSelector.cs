using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GhostFaceSelector : MonoBehaviour
{
    private MeshRenderer _mesh;

    public Material Normal, Sad, Angry, Happy, Surprised, Tired;


    public void SetNormal() => SetFace(Normal);
    public void SetSad() => SetFace(Sad);
    public void SetAngry() => SetFace(Angry);
    public void SetHappy() => SetFace(Happy);
    public void SetSurprised() => SetFace(Surprised);
    public void SetTired() => SetFace(Tired);

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    private void SetFace(Material mat)
    {
        Debug.Log("Attempt to change material to " + mat);
        _mesh.material = mat;
    }
}
