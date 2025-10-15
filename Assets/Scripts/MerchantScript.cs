using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MerchantScript : MonoBehaviour
{

    public Animator animator;
    public GameObject image;
    [SerializeField] private int nMerchant;
    [SerializeField] private bool inRange;
    private PlayerScript player;
    private Shooting weapon;
    private AudioSource audio;

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float[] maxDistance;
    private Vector2 waypoint;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shooting>();
        animator = image.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        inRange = false;
        //image.SetActive(false);

        SetNewDestination();
    }


    private void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            switch (nMerchant)
            {
                case 1:
                    if(player.tearCount >= 10)
                    {
                        audio.pitch = Random.Range(1, 2);
                        audio.volume = Random.Range(.6f, 1);
                        audio.Play();
                        player.tearCount -= 10;
                        weapon.gunAmmo += 20;
                    }
                    break;
                case 2:
                    if (player.tearCount >= 20)
                    {
                        audio.pitch = Random.Range(1, 2);
                        audio.volume = Random.Range(.6f, 1);
                        audio.Play();
                        player.tearCount -= 20;
                        weapon.shotGunAmmo += 20;
                    }
                    break;
                case 3:
                    if (player.tearCount >= 20)
                    {
                        audio.pitch = Random.Range(1, 2);
                        audio.volume = Random.Range(.6f, 1);
                        audio.Play();
                        player.tearCount -= 20;
                        weapon.ak47Ammo += 20;
                    }
                    break;
                case 4:
                    if (player.tearCount >= 20)
                    {
                        audio.pitch = Random.Range(1, 2);
                        audio.volume = Random.Range(.6f, 1);
                        audio.Play();
                        player.tearCount -= 20;
                        weapon.bazookaAmmo += 10;
                    }
                    break;
            }    
        }
        if(!inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoint) < range)
            {
                SetNewDestination();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("inRange", true);
            inRange = true;
            //image.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("inRange", false);
            inRange = false;
            //image.SetActive(false);
        }
    }
    private void SetNewDestination()
    {
        waypoint = new Vector2(Random.Range(maxDistance[0], maxDistance[1]), Random.Range(maxDistance[2], maxDistance[3]));
    }

}
