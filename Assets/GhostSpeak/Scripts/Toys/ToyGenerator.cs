using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

public class ToyGenerator : MonoBehaviour
{
    public ToyItem[] Items;

    public int GenerationCount = 1;

    private void Start()
    {
        ToyItem[] shuffled = Items.OrderBy(i => UnityEngine.Random.value).ToArray();

        for (int i = 0; i < Math.Min(GenerationCount, Items.Length); i++)
            Instantiate(shuffled[i], transform);
    }
}
