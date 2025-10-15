using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator animator;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        animator.GetComponent<Animator>();
    }

    public void NewLevel(int level)
    {
        StartCoroutine(WaitLevel(level));
    }


    private IEnumerator WaitLevel(int level)
    {
        if(level < 16)
        {
            yield return new WaitForSeconds(2);
            animator.SetTrigger("start");
            text.text = "LEVEL  " + level;
        }
    }
}
