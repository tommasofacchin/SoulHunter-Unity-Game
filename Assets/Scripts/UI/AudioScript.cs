using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public GameObject audioOn;
    public GameObject audioOff;

    private bool isActive;

    private void Start()
    {
        isActive = true;
    }
    public void Change()
    {
        Debug.Log("log");
        isActive = !isActive;

        audioOn.SetActive(isActive);
        audioOff.SetActive(!isActive);

        AudioListener.pause = !isActive;
    }
}
