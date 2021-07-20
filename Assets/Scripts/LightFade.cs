using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    Light fadeLight;

    [SerializeField] float fadeSpeed = 1.5f;
    [SerializeField] float offset = 0f;

    float maxIntensity;

    // Start is called before the first frame update
    void Start()
    {
        fadeLight = GetComponent<Light>();
        maxIntensity = fadeLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        Fade();
    }

    void Fade()
    {
        if (Time.time > offset) fadeLight.intensity = Mathf.PingPong((Time.time * fadeSpeed) , maxIntensity);
    }
}
