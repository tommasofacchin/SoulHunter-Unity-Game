using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{


    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;
    public bool isBoss;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (isBoss) speed = 10;
        else speed = 5;
        direction = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2 (direction.x, direction.y);
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerScript>().takeDamage(10);
            Destroy(gameObject);
        }
    }
}
