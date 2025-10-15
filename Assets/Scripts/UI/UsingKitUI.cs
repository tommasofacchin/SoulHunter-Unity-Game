using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingKitUI : MonoBehaviour
{

    public Image usingKitBar;
    public float progress;


    [Header("Scripts")]
    public PlayerScript player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        progress = (player.useKitCounter / 250);
        usingKitBar.fillAmount = progress;
    }

}
