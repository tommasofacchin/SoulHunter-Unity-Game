using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEClick : MonoBehaviour
{

    public GameObject chest;
    private ChestScript chestScript;


    private void Awake()
    {
        chestScript = chest.GetComponent<ChestScript>();
    }

    private void OnMouseDown()
    {
        chestScript.isClick = true;
    }
    private void OnMouseUp()
    {
        chestScript.isClick = false;
    }
}
