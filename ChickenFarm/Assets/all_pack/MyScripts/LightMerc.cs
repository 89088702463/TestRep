using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMerc : MonoBehaviour
{
    public float duration = 1.5f;
    public Light lt;

    private void Start()
    {
        lt = GetComponent<Light>();
    }
    //public GameObject lamp;
    //public float Timerr = 15;

    void Update()
    {

        float phi = Time.time / duration * 4 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 8.6F + 25.75F;
        lt.intensity = amplitude;

    }
}
