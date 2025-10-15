using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimator : MonoBehaviour
{

    public Animator animator;
    public PlayerScript player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.position != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if(player.useKitCounter > 0)
        {
            animator.SetBool("isMoving", true);
        }


    }


}
