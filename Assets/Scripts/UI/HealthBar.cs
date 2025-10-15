using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image health;
    public float progress;

    [Header("Scripts")]
    public PlayerScript player;

    private void Awake()
    {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
        progress = player.lp/100;
        health.fillAmount = progress;
    }
}
