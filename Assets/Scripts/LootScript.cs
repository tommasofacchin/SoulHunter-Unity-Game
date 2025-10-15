using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootScript : MonoBehaviour
{


    public float force;
    public Animator animator;


    public AudioSource takingSound;
    public ParticleSystem particles;

    private Vector2 dropDirection;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(dropDirection * 300f, ForceMode2D.Impulse);

        Destroy(gameObject, 15f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            takingSound.pitch = Random.Range(1, 2);
            takingSound.volume = Random.Range(.6f, 1);
            takingSound.Play();
            animator.SetTrigger("Destroy");
            particles.Play();
            Destroy(gameObject, 1f);
        }
    }

    /*
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Invoke("Deactivate", 15f);
        taken = false;
        dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(dropDirection * 300f, ForceMode2D.Impulse);
    }
    */

}
