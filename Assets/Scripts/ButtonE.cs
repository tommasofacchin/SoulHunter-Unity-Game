using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonE : MonoBehaviour
{

    private Animator animator;


    public GameObject chest;
    private ChestScript chestScript;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        chestScript = chest.GetComponent<ChestScript>();
        chestScript.inRange = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetBool("inRange", true);
            chestScript.inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           animator.SetBool("inRange", false);
            chestScript.inRange = false;
        }
    }


}
