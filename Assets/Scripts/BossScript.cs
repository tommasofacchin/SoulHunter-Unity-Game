using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    private float stoppingDistance;
    private PlayerScript player;
    private Transform target;
    private float timer;
    private float waitForAttack;
    private Animator animator;
    [SerializeField] public bool canShoot;

    private EnemySounds sound;
    public AudioSource audioSource;




    private WeaponScript weaponScript;
    private Collider2D collider;
    private Collider2D spriteCollider;
    private Animator shadowAnimator;
    [SerializeField] public Transform enemySprite;
    [SerializeField] public GameObject shadow;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject shootingPoint;
    [SerializeField] public GameObject firstSprite;
    [SerializeField] private LayerMask enemyLayer;


    private GameObject Player;
    private GameObject weapon;


    public GameObject healthBar;
    private BossHealthBar bar;

    public float maxLp;
    public float lp;
    public float maxSpeed;
    public float speed;
    public float damage;
    private bool isAlive;
    public float shootDelay;
    private float shootTimer;

    public float teleportDelay;
    private float teleportTimer;
    private bool canTeleport = true;

    public Vector3 teleportSpot = new Vector3();


    public GameObject finalScreen;
    public GameObject UI;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        weapon = GameObject.FindWithTag("Weapon");
        target = Player.GetComponent<Transform>();
        player = Player.GetComponent<PlayerScript>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        shadowAnimator = shadow.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        sound = GetComponent<EnemySounds>();
        animator = enemySprite.GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        spriteCollider = firstSprite.GetComponent<PolygonCollider2D>();
        lp = maxLp;
        waitForAttack = .5f;
        isAlive = true;
        speed = maxSpeed;

        audioSource.volume = Random.Range(.2f, .4f);
        audioSource.pitch = Random.Range(.8f, 1.2f);

        teleportTimer = 2;

        bar = healthBar.GetComponent<BossHealthBar>();
        bar.maxValue = (int)maxLp;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance && !player.isHidden)
        {
            float lerpValue = Mathf.Clamp01(speed * Time.deltaTime / Vector2.Distance(transform.position, target.position));
            transform.position = Vector2.Lerp(transform.position, target.position, lerpValue);
        }

        if (canShoot)
        {
            if (shootTimer <= 0.0f)
            {
                if (lp > 0 && !player.isHidden && isAlive && Vector3.Distance(target.position, transform.position) < 9)
                {
                    Shoot(); 
                    shootTimer = shootDelay; 
                }
            }

            if (shootTimer > 0.0f)
            {
                shootTimer -= Time.deltaTime;
            }
        }

        if (canTeleport && !player.isHidden && lp > 0)
        {
            if(teleportTimer <= 0.0f)
            {
                animator.SetTrigger("Teleport");
                shadowAnimator.SetTrigger("Teleport");
                Invoke("Teleport", .1f);
                teleportTimer = teleportDelay;
            }
            else
            {
                teleportTimer -= Time.deltaTime;    
            }
        }


        if (player.isHidden)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }


        if(lp <= 0)
        {
            player.won = true;
            //KillEnemies();
            Invoke("PauseGame", 1f);
            animator.SetBool("Dead", true);
        }
    }


    private void Shoot()
    {
        Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
    }

    private void Teleport()
    {

        if (Random.Range(0, 2) == 1) teleportSpot.x = target.position.x + 4;
        else teleportSpot.x = target.position.x - 4;
        if (Random.Range(0, 2) == 1) teleportSpot.y = target.position.y + 4;
        else teleportSpot.y = target.position.y - 4;

        transform.position = teleportSpot;
        audioSource.volume = .2f;
        audioSource.pitch = Random.Range(.9f, 1.2f);
        audioSource.PlayOneShot(sound.hit2);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAlive)
        {
            if (timer >= waitForAttack && lp > 0)
            {
                collision.gameObject.GetComponent<PlayerScript>().takeDamage(damage);
                audioSource.PlayOneShot(sound.attack);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet") && isAlive)
        {
            col.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        lp -= weaponScript.damage;
        bar.Change((int)weaponScript.damage);
        if (lp <= 0 && isAlive)
        {

            audioSource.volume = .8f;
            audioSource.PlayOneShot(sound.dying);
            isAlive = false;
            speed = 0;
            animator.SetBool("dead", true);
            shadow.SetActive(false);
            collider.enabled = false;
            spriteCollider.enabled = false;

        }
    }
    public void TakeDamage(float damage)
    {
        lp -= damage;
        bar.Change((int)damage);
        if (lp <= 0 && isAlive)
        {

             audioSource.volume = .8f;
             audioSource.PlayOneShot(sound.dying);
             isAlive = false;
             speed = 0;
             animator.SetBool("dead", true);
             shadow.SetActive(false);
             collider.enabled = false;
             spriteCollider.enabled = false;

        }
    }

    private void PauseGame()
    {
        UI.SetActive(false);
        canTeleport = false;
        finalScreen.SetActive(true);
        //Invoke("StopTime", 1f);
        Destroy(gameObject, 1f);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("gunAmmo", 300);
        PlayerPrefs.SetInt("shotGunAmmo", 200);
        PlayerPrefs.SetInt("ak47Ammo", 400);
        PlayerPrefs.SetInt("bazookaAmmo", 400);
        PlayerPrefs.SetFloat("lp", 100);
        PlayerPrefs.SetInt("cutscene", 0);
    }
    private void StopTime()
    {
        Time.timeScale = 0f;
    }
    private void KillEnemies()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject obj in objectsWithTag)
        {
            EnemyScript script = obj.GetComponent<EnemyScript>();

            if (script != null)
            {
                script.canRespawn = false;
                script.TakeDamage(1000);
            }
        }
    }
}
