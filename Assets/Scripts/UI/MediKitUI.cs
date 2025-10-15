using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediKitUI : MonoBehaviour
{

    public GameObject kit1;
    public GameObject kit2;
    public GameObject kit3;

    private PlayerScript player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void Update()
    {

        if (player.mediKitCounter == 0)
        {
            kit1.SetActive(false);
            kit2.SetActive(false);
            kit3.SetActive(false);
        }
        if (player.mediKitCounter == 1)
        {
            kit1.SetActive(true);     
            kit2.SetActive(false);     
            kit3.SetActive(false);     
        }
        if (player.mediKitCounter == 2)
        {
            kit1.SetActive(true);
            kit2.SetActive(true);
            kit3.SetActive(false);
        }
        if (player.mediKitCounter == 3)
        {
            kit1.SetActive(true);
            kit2.SetActive(true);
            kit3.SetActive(true);
        }

    }
}
