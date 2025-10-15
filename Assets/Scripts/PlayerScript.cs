using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Windows.Speech;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D Rigidbody;
    public Vector3 position;
    private PolygonCollider2D col;
    [Header("STATS")]
    public float maxLp;
    public float lp;
    public float speed;
    [Space(20)]

    public float totalKillCount;
    public float killCount;
    public float addLpCounter;
    public float soulCount;
    public float tearCount;
    public Vector3 spawnPoint;
    public bool isHidden;
    private bool inSnow = false;
    public bool snowArmor = false;
    public bool canArmor;
    public int mediKitCounter;
    public float useKitCounter;

    public FixedJoystick joystick;

    public ParticleSystem dust;

    private bool canDash;
    private float dashForce;

    public bool won = false;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        isHidden = false;
        /*
        lp = maxLp;
        mediKitCounter = 0;
        addLpCounter = 0;
        soulCount = 0;
        tearCount = 0;
        */
        canDash = true;
        dashForce = 1;
    }
    private void FixedUpdate()
    {
        position = Vector2.zero;
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
        //position.x = joystick.Horizontal;
        //position.y = joystick.Vertical;


        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 5);

        if (position != Vector3.zero && useKitCounter == 0 && !won)
        {
            CreateDust();
            Movement();
        }



        if (inSnow && !snowArmor)
        {
            lp -= .02f;
            if (lp <= 0)
            {
                Die();
            }
        }
        if(Input.GetKey(KeyCode.F) && lp < 100 && mediKitCounter > 0){
                useKitCounter++;
        }
        else
        {
            useKitCounter = 0;
        }

        if(useKitCounter == 250)
        {
            addLpCounter = 25;
            mediKitCounter--;
            useKitCounter = 0;
        }


        if(addLpCounter > 0)
        {
            addLpCounter--;
            lp += 1;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (canDash)
            {
                Dash();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Soul"))
        {
            soulCount++;
        }

        if (collision.CompareTag("tear"))
        {
            tearCount++;
        }
        if (collision.CompareTag("Snow"))
        {
            inSnow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Snow"))
        {
            inSnow = false;
        }

    }


    private void Movement()
    {
        Rigidbody.MovePosition(transform.position + position * speed * dashForce * Time.deltaTime);
    }

    public void takeDamage(float damage)
    {
        if (snowArmor)
        {
            damage *= 1.5f;
        }
        lp -= damage;
        if (lp <= 0)
        {
            Die();
        }
    }
    public void Knockback(Vector3 pushDirection)
    {
        GetComponent<Rigidbody>().AddForce(pushDirection);
    }

    private void AddLp(float addLp)
    {
        lp += addLp;

        if (lp > 100)
        {
            lp = 100;
        }
    }

    private void Die()
    {
        lp = maxLp;
        mediKitCounter = 0;
        spawnPoint = new Vector3(Random.Range(-15, 90), Random.Range(-50, 10), 0);
        transform.position = spawnPoint;
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void Dash()
    {
        canDash = false;
        Invoke("DashDelay", 2f);
        Invoke("RemoveDash", .2f);
        dashForce = 3f;
        col.enabled = false;
    }
    private void DashDelay()
    {
        canDash = true;
    }
    private void RemoveDash()
    {
        dashForce = 1f;
        col.enabled = true;
    }
}
