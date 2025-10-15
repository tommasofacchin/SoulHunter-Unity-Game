using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject zombie;
    [SerializeField] public GameObject zombieDecapitated;
    [SerializeField] public GameObject skeleton;
    [SerializeField] public GameObject archer;
    [SerializeField] public GameObject GiantZombie;
    [SerializeField] public GameObject Merchant1;
    [SerializeField] public GameObject Merchant2;
    [SerializeField] public GameObject Merchant3;
    [SerializeField] public GameObject Merchant4;
    [SerializeField] private GameObject barrelPrefab;

    //[SerializeField] public float intervalZombie, intervalSkeleton, intervalArcher;
    private Vector3 temp;


    public GameObject player;
    private PlayerScript playerScript;

    public LoadLevel level;
    public int currentLevel;



    public GameObject dialogue;
    private DialogueScript dialogueScript;


    //public int maxEnemies;
    public int toKill;


    public GameObject chestShotgun;
    public GameObject chestAk47;
    public GameObject chestArmor;
    public GameObject chestBazooka;
    public GameObject Boss;
    public GameObject bossHealth;

    private void Awake()
    {
        playerScript = player.GetComponent<PlayerScript>();
        dialogueScript = dialogue.GetComponent<DialogueScript>();

        currentLevel = PlayerPrefs.GetInt("currentLevel");
        toKill = PlayerPrefs.GetInt("toKill");

        SpawnEnemy(zombie, 8);
        Invoke("SpawnBarrel", 60);
    }

    private void Start()
    {
        level.NewLevel(currentLevel);
        if (currentLevel == 2)
        {
            chestShotgun.SetActive(true);
            Merchant2.SetActive(true);
        }
        else if(currentLevel == 3)
        {
            chestShotgun.SetActive(true);
            chestArmor.SetActive(true);
            Merchant3.SetActive(true);
            SpawnEnemy(zombie, 3);
            SpawnEnemy(skeleton, 2);
        }
        else if (currentLevel == 4)
        {
            chestShotgun.SetActive(true);
            chestAk47.SetActive(true);
            chestArmor.SetActive(true);
            Merchant2.SetActive(true);
            Merchant3.SetActive(true);
        }
        else if(currentLevel == 5)
        {
            chestShotgun.SetActive(true);
            chestAk47.SetActive(true);
            chestArmor.SetActive(true);
            Merchant2.SetActive(true);
            Merchant3.SetActive(true);
            SpawnEnemy(zombie, currentLevel * 2);
            SpawnEnemy(skeleton, currentLevel);
            SpawnEnemy(archer, currentLevel);
        }
        else if (currentLevel > 5)
        {
            chestShotgun.SetActive(true);
            chestAk47.SetActive(true);
            chestArmor.SetActive(true);
            chestBazooka.SetActive(true);
            Merchant2.SetActive(true);
            Merchant3.SetActive(true);
            Merchant4.SetActive(true);
            SpawnEnemy(zombie, currentLevel * 2);
            SpawnEnemy(skeleton, currentLevel);
            SpawnEnemy(archer, currentLevel);
        }
        
        if(currentLevel >= 15)
        {
            Invoke("StartBoss", 5f);
            dialogueScript.newDialogue("The demon has arrived.");
        }
    }
    private void FixedUpdate()
    {
        if(playerScript.killCount >= toKill)
        {
            //Resources.UnloadUnusedAssets();
            playerScript.killCount = 0; 

            currentLevel++;
            toKill += 10;

            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.SetInt("toKill", toKill);

            level.NewLevel(currentLevel);


            switch (currentLevel)
            {
                case 2:
                    chestShotgun.SetActive(true);
                    Merchant2.SetActive(true);
                    SpawnEnemy(zombie, 3);
                    SpawnEnemy(skeleton, 2);

                    dialogueScript.newDialogue
                        ("A new chest has appeared, you can find it in the northern forest.");
                    break;
                case 3:
                    chestArmor.SetActive(true);
                    Merchant3.SetActive(true);
                    SpawnEnemy(zombie, 3);
                    SpawnEnemy(skeleton, 2);
                    dialogueScript.newDialogue
                        ("A new chest has appeared, you can find it in the city.");
                    break;
                case 4:
                    chestAk47.SetActive(true);
                    SpawnEnemy(zombie, 3);
                    SpawnEnemy(skeleton, 2);
                    SpawnEnemy(archer, 3);
                    dialogueScript.newDialogue
                        ("A new chest has appeared, you can find it in a really cold place, so make sure you bring the appropriate suit.");
                    break;
                case 5:
                    SpawnEnemy(zombie, 3);
                    SpawnEnemy(skeleton, 2);
                    SpawnEnemy(archer, 1);
                    SpawnEnemy(GiantZombie, 1);
                    break;
                case 6:
                    chestBazooka.SetActive(true);
                    SpawnEnemy(zombie, 3);
                    SpawnEnemy(skeleton, 2);
                    SpawnEnemy(archer, 1);
                    SpawnEnemy(GiantZombie, 1);
                    dialogueScript.newDialogue
                        ("A new chest has appeared, you can find it in a really cold place, so make sure you bring the appropriate suit.");
                    break;
                case 15:
                    Invoke("StartBoss", 5f);
                    dialogueScript.newDialogue
                        ("The demon has arrived.");
                    break;
            }
            if(currentLevel > 6)
            {
                SpawnEnemy(zombie, 3);
                SpawnEnemy(skeleton, 2);
                SpawnEnemy(archer, 1);
                SpawnEnemy(GiantZombie, 1);
            }



        }
    }

    private void StartBoss()
    {
        Boss.SetActive(true);
        bossHealth.SetActive(true);
    }
    void SpawnEnemy(GameObject enemy, int n)
    {

        do
        {
            temp.x = Random.Range(-14f, 70f);
            temp.y = Random.Range(-49f, 19f);
        }
        while (Vector3.Distance(temp, player.transform.position) < 20);


        GameObject newEnemy = Instantiate(enemy, temp, Quaternion.identity);
        if (n > 0)
        {
            SpawnEnemy(enemy, --n);
        }
    }

    private void SpawnBarrel()
    {
        Vector2 barrelPosition = new Vector2(Random.Range(10, 150), Random.Range(-40, 10));
        Instantiate(barrelPrefab, barrelPosition, Quaternion.identity);
        Invoke("SpawnBarrel", 60);
    }
}
