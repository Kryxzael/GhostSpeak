using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.transform.GetComponent<ToyItem>() is ToyItem toy)
                {
                    toy.OnClick();
                }
            }
        }
    }
}
