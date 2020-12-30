using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light), typeof(AudioSource))]
public class Lightning : MonoBehaviour
{
    public float MinDelay = 10f;
    public float MaxDelay = 45f;

    private IEnumerator Start()
    {
        AudioSource thunder = GetComponent<AudioSource>();
        Light lightning = GetComponent<Light>();

        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(MinDelay, MaxDelay));

            int strikeCount = UnityEngine.Random.Range(1, 4);
            thunder.Play();
            for (int i = 0; i < strikeCount; i++)
            {
                lightning.enabled = true;

                yield return new WaitForSeconds(0.2f);
                lightning.enabled = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
