using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class EnemyScript : MonoBehaviour
{

    private float stoppingDistance;
    private PlayerScript player;
    private Transform target;
    private float timer;
    private float waitForAttack;
    private Animator animator;
    private Animator bowAnimator;
    private Animator secondAnimator;
    [SerializeField] public bool canShoot;

    private EnemySounds sound;
    public AudioSource audioSource;

    public int killToSpawn;



    [SerializeField] public Shooting shooting;
    [SerializeField] public WeaponScript weaponScript;
    [SerializeField] public Transform enemySprite;
    [SerializeField] public Collider2D collider;
    [SerializeField] public Collider2D spriteCollider;
    [SerializeField] public Collider2D spriteCollider2;
    [SerializeField] public GameObject shadow;
    [SerializeField] public GameObject secondShadow;
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject shootingPoint;
    [SerializeField] public GameObject firstSprite;
    [SerializeField] public GameObject secondSprite;
    [SerializeField] private GameObject tearPrefab;
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private LayerMask enemyLayer;


    private GameObject Player;
    private GameObject weapon;


    public float maxLp;
    public float lp;
    public float maxSpeed;
    public float speed;
    public float damage;
    private bool isAlive;
    private float shootDelay;
    private float shootTimer;


    public bool doubleLife;
    private bool isDoubleLife;

    public bool canRespawn;

    public GameObject ammo;
    public GameObject mediKit;

    public GameObject healthBar;
    private EnemyHealthBar bar;

    private Vector3 temp;


    [SerializeField] private float[] maxDistance;
    private Vector2 waypoint;

    public GameObject explosion;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        weapon = GameObject.FindWithTag("Weapon");
        target = Player.GetComponent<Transform>();
        player = Player.GetComponent<PlayerScript>(); 
        weaponScript = weapon.GetComponent<WeaponScript>();
        shooting = weapon.GetComponent<Shooting>();


        audioSource = GetComponent<AudioSource>();
        sound = GetComponent<EnemySounds>();
        animator = enemySprite.GetComponent<Animator>();
        bowAnimator = shootingPoint.GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        spriteCollider = firstSprite.GetComponent<PolygonCollider2D>();
        bar = healthBar.GetComponent<EnemyHealthBar>();
        healthBar.SetActive(false);
        lp = maxLp;
        waitForAttack = .5f;
        stoppingDistance = 0;
        shootDelay = 5f;
        isAlive = true;
        bar.maxValue = (int)maxLp;
        isDoubleLife = doubleLife;
        speed = maxSpeed;
        Physics2D.IgnoreLayerCollision(7, 11);
        Physics2D.IgnoreLayerCollision(7, 5);
        waypoint = new Vector2(Random.Range(maxDistance[0], maxDistance[1]), Random.Range(maxDistance[2], maxDistance[3]));

        audioSource.volume = Random.Range(.2f, .4f);
        audioSource.pitch = Random.Range(.8f, 1.2f);


    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance && !player.isHidden)
        {
            float lerpValue = Mathf.Clamp01(speed * Time.deltaTime / Vector2.Distance(transform.position, target.position));
            transform.position = Vector2.Lerp(transform.position, target.position, lerpValue);
        }
        else if (player.isHidden)
        {

            animator.SetBool("isMoving", false);
            transform.position = Vector2.MoveTowards(transform.position, waypoint, speed / 3 * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoint) < 10)
            {
                waypoint = new Vector2(Random.Range(maxDistance[0], maxDistance[1]), Random.Range(maxDistance[2], maxDistance[3]));
            }
        }
        else
        {
            animator.SetBool("isMoving", true);
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





    }


    private void Shoot()
    {
        Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
    }

    


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isAlive)
        {
            if (timer >= waitForAttack && lp>0)
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

    public async void TakeDamage()
    {
        lp -= weaponScript.damage;
        healthBar.SetActive(true);
        bar.Change((int)weaponScript.damage);
        if (lp <= 0 && isAlive)
        {
            if (isDoubleLife)
            {
                GetSecondLife();
            }
            else
            {
                if (canShoot)
                {
                    Destroy(gameObject, 1f);
                    SpawnEnemy(goblinPrefab, 1);
                }

                audioSource.volume = .8f;
                audioSource.PlayOneShot(sound.dying);
                isAlive = false;
                speed = 0;
                animator.SetBool("dead", true);
                bowAnimator.SetBool("dead", true);
                shadow.SetActive(false);
                collider.enabled = false;
                spriteCollider.enabled = false;
                Drop();
                await Deactivate();

                if (doubleLife)
                {
                    secondAnimator.SetBool("dead", true);
                    secondShadow.SetActive(false);
                    spriteCollider2.enabled = false;
                }
            }

        }
        if(weaponScript.currentWeapon == 4)
        {
                Explode();
        }
    }
    public async void TakeDamage(float damage)
    {
        lp -= damage;
        healthBar.SetActive(true);
        bar.Change((int)damage);
        if (lp <= 0 && isAlive)
        {
            if (isDoubleLife)
            {
                GetSecondLife();
            }
            else
            {
                if (canShoot)
                {
                    Destroy(gameObject, 1f);
                    SpawnEnemy(goblinPrefab, 1);
                }

                audioSource.volume = .8f;
                audioSource.PlayOneShot(sound.dying);
                isAlive = false;
                speed = 0;
                animator.SetBool("dead", true);
                bowAnimator.SetBool("dead", true);
                shadow.SetActive(false);
                collider.enabled = false;
                spriteCollider.enabled = false;
                //GetComponent<LootBag>().InstantiateLoot(transform.position);
                Drop();
                if (!canShoot)
                {
                    await Deactivate();
                }

                if (doubleLife)
                {
                    secondAnimator.SetBool("dead", true);
                    secondShadow.SetActive(false);
                    spriteCollider2.enabled = false;
                }
            }

        }
    }


    private void Drop()
    {
        int random;

        if(shooting.gunAmmo > 300)
        {
            random = Random.Range(0, 14);
        }
        else
        {
            random = Random.Range(0, 5);
        }

        if (random == 1)
        {
            Instantiate(ammo, transform.position, Quaternion.Euler(0, 0, 0));
        }


        int randomMediKit = Random.Range(0, 100);

        if (randomMediKit == 1)
        {
            Instantiate(mediKit, transform.position, Quaternion.Euler(0, 0, 0));
        }


        Instantiate(soulPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        int randomTear = Random.Range(1, 11);

        for (int i = 0; i < randomTear; i++)
        {

            Instantiate(tearPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    async Task Deactivate()
    {
        if(canRespawn) await Task.Delay(1000);
        healthBar.SetActive(false);
        player.killCount++;
        player.totalKillCount++;
        if(!canShoot)
        {
            await Activate();
        }

        do
        {
            temp.x = Random.Range(-14f, 170f);
            temp.y = Random.Range(-49f, 19f);
        }
        while (Vector3.Distance(temp, player.transform.position) < 20);
        transform.position = temp;
    }
        
    async Task Activate()
    {
        await Task.Delay(2000);

            do
            {
                temp.x = Random.Range(-14f, 170f);
                temp.y = Random.Range(-49f, 19f);
            }
            while (Vector3.Distance(temp, player.transform.position) < 20);


        if (doubleLife)
        {
            firstSprite.SetActive(true);
            secondSprite.SetActive(false);
            secondAnimator.SetBool("dead", false);
            secondShadow.SetActive(true);
            spriteCollider2.enabled = true;
        }
        if (!canShoot)
        {
            if (audioSource != null) audioSource.volume = Random.Range(.2f, .4f);
            bowAnimator.SetBool("dead", false);
            animator.SetBool("dead", false);
            shadow.SetActive(true);
            isAlive = true;
            collider.enabled = true;
            spriteCollider.enabled = true;
            lp = maxLp;
            speed = maxSpeed;
            isDoubleLife = doubleLife;
            bar.value = (int)maxLp;
            bar.redBar.sizeDelta = new Vector2(bar.fullWidth, bar.redBar.rect.height);
            bar.bar.sizeDelta = new Vector2(bar.fullWidth, bar.bar.rect.height);
 
        }
    }

    void GetSecondLife()
    {
        firstSprite.SetActive(false);
        secondSprite.SetActive(true);
        lp = maxLp;
        bar.Change((int)-lp);
        isDoubleLife = false;
    }

    void SpawnEnemy(GameObject enemy, int n)
    {

        do
        {
            temp.x = Random.Range(-14f, 70f);
            temp.y = Random.Range(-49f, 19f);
        }
        while (Vector3.Distance(temp, player.transform.position) < 20);

        Instantiate(enemy, temp, Quaternion.identity);
        if (--n > 0)
        {
            SpawnEnemy(enemy, n);
        }
    }
    private void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll
            (transform.position, 2f, enemyLayer);

        Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(25f);
            }
        }
    }
}

