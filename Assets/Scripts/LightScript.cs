using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightScript : MonoBehaviour
{
    public float minIntensity;
    public float maxIntensity;
    public float speed;
    public Light2D light;

    private float startTime;
    private float randomOffset;

    void Start()
    {
        // Inizializza l'offset casuale in modo che le luci abbiano valori iniziali diversi.
        randomOffset = Random.Range(0f, 1000f);
        startTime = Time.time;
    }

    void Update()
    {
        // Calcola l'intensità basata su PerlinNoise per ottenere un valore pseudo-randomico.
        float t = (Time.time - startTime) * speed + randomOffset;
        float noise = Mathf.PerlinNoise(t, 0f);
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        // Imposta l'intensità della luce.
        light.intensity = intensity;

        // Controlla se è il momento di cambiare il targetIntensity.
        if (Time.time - startTime > speed)
        {
            // Imposta un nuovo targetIntensity casuale.
            randomOffset = Random.Range(0f, 1000f);
            startTime = Time.time;
        }
    }
}
