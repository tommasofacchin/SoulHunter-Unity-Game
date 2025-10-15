using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    public Image bar;
    public float progress;
    public GameObject spawner;


    [Header("Scripts")]
    private PlayerScript player;
    private EnemySpawner spawnerScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        spawnerScript = spawner.GetComponent<EnemySpawner>();
    }
    void Update()
    {
        progress = player.killCount / spawnerScript.toKill;
        bar.fillAmount = progress;
    }
}
