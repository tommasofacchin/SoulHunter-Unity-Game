using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public GameObject player;
    private ParticleSystem particle;
    ParticleSystem.MainModule main;
    
    public GameObject image;
     private PlayerScript playerScript;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        playerScript = player.GetComponent<PlayerScript>();
        main = particle.main;
    }

    void Update()
    {
        if(player.transform.position.x > 95)
        {
            main.startColor = Color.white;
            if(!playerScript.snowArmor)image.SetActive(true);
        }
        else
        {
            main.startColor = new Color(255f / 255.0f, 184f / 255.0f, 81f / 255.0f, 1f);
            image.SetActive(false);
        }


    }
}
