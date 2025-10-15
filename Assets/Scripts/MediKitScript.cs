using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediKitScript : MonoBehaviour
{

    private PlayerScript player;
    public AudioSource audio;
    public GameObject sprite;
    private Animator animator;
    public GameObject shadow;
    private bool taken = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        animator = sprite.GetComponent<Animator>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && player.mediKitCounter<3 && !taken)
        {
            taken = true;
            player.mediKitCounter++;
            audio.Play();
            animator.SetTrigger("Destroy");
            Destroy(shadow);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100, ForceMode2D.Impulse);
            StartCoroutine(Destroying());
        }
    }


    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
