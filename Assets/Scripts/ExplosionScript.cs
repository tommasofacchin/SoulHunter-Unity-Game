using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = Random.Range(.2f, .3f);
        audio.pitch = Random.Range(.5f, 3);
        Destroy(gameObject, 1f);
    }
}
