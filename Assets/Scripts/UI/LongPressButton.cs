using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPressButton : MonoBehaviour
{


    private bool isClick;

    public PlayerScript player;
    public AudioSource audio;

    private void OnMouseDown()
    {
        isClick = true;
    }
    private void OnMouseUp()
    {
        isClick=false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void Update()
    {

        if (isClick && player.lp < 100 && player.mediKitCounter > 0)
        {
            player.useKitCounter += 2;
        }
        else if (player.useKitCounter > 0) player.useKitCounter -= 3;


        if (player.useKitCounter == 250)
        {
            audio.Play();
            player.addLpCounter = 25;
            player.mediKitCounter--;
            player.useKitCounter = 0;
        }


        if (player.addLpCounter > 0)
        {
            player.addLpCounter--;
            player.lp += 1;
        }


    }


}
