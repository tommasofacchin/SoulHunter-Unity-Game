using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeArmor : MonoBehaviour
{



    private PlayerScript playerScript;
    public GameObject armor;
    public GameObject player;
    public GameObject armorImage;

    private void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    public void Change()
    {
        if (playerScript.canArmor)
        {
            if (armor.activeSelf)
            {
                armorImage.SetActive(false);
                armor.SetActive(false);
                playerScript.snowArmor = false;
            }
            else
            {
                armorImage.SetActive(true);
                armor.SetActive(true);
                playerScript.snowArmor = true;
            }
        }


    }
}
