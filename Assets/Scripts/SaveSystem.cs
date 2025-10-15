using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;

    public GameObject weapon;
    private Shooting shooting;
    private WeaponScript weaponScript;

    public GameObject spawner;
    private EnemySpawner enemySpawner;

    private Vector3 position;
    public int chestCount;
    public GameObject chestShotgun;
    public GameObject chestAk47;
    public GameObject chestArmor;
    public GameObject chestBazooka;
    private ChestScript shotgun;
    private ChestScript ak47;
    private ChestScript armor;
    private ChestScript bazooka;

    private void Start()
    {

        playerScript = player.GetComponent<PlayerScript>();
        shooting = weapon.GetComponent<Shooting>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        enemySpawner = spawner.GetComponent<EnemySpawner>();
        shotgun = chestShotgun.GetComponent<ChestScript>();
        ak47 = chestAk47.GetComponent<ChestScript>();
        armor = chestArmor.GetComponent<ChestScript>();
        bazooka = chestBazooka.GetComponent<ChestScript>();

        playerScript.soulCount = PlayerPrefs.GetFloat("soulCount");
        playerScript.tearCount = PlayerPrefs.GetFloat("tearCount");
        playerScript.mediKitCounter = PlayerPrefs.GetInt("mediKitCounter");
        playerScript.lp = PlayerPrefs.GetFloat("lp");
        playerScript.totalKillCount = PlayerPrefs.GetFloat("totalKillCount");
        playerScript.killCount = PlayerPrefs.GetFloat("killCount");
        position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), 0f);
        player.transform.position = position;

        shooting.gunAmmo = PlayerPrefs.GetInt("gunAmmo");
        shooting.shotGunAmmo = PlayerPrefs.GetInt("shotGunAmmo");
        shooting.ak47Ammo = PlayerPrefs.GetInt("ak47Ammo");
        shooting.bazookaAmmo = PlayerPrefs.GetInt("bazookaAmmo");

        enemySpawner.toKill = PlayerPrefs.GetInt("toKill");
        enemySpawner.currentLevel = PlayerPrefs.GetInt("currentLevel");



        chestCount = PlayerPrefs.GetInt("chestCount");


        switch (enemySpawner.currentLevel)
        {
            case 1:
                break;
            case 2:
                chestShotgun.SetActive(true);
                break;
            case 3:
                chestShotgun.SetActive(true);
                chestArmor.SetActive(true);
                break;
            case 4:
                chestShotgun.SetActive(true);
                chestArmor.SetActive(true);
                chestAk47.SetActive(true);
                break;
            case 6:
                chestShotgun.SetActive(true);
                chestArmor.SetActive(true);
                chestAk47.SetActive(true);
                chestBazooka.SetActive(true);
                break;

        }

        switch (chestCount)
        {
            case 0:
                break;
            case 1:
                shotgun.isOpened();
                break;
            case 2:
                shotgun.isOpened();
                armor.isOpened();
                break;
            case 3:
                shotgun.isOpened();
                armor.isOpened();
                ak47.isOpened();
                break;
            case 4:
                shotgun.isOpened();
                armor.isOpened();
                ak47.isOpened();
                bazooka.isOpened();
                break;
        }


        if (PlayerPrefs.GetInt("canArmor") == 1)
        {
            playerScript.canArmor = true;
        }

    }




    public void newGame()
    {
        
        player.transform.position = playerScript.spawnPoint;


        shooting.gunAmmo = 300;
        shooting.shotGunAmmo = 200;
        shooting.ak47Ammo = 400;

        enemySpawner.currentLevel = 1;
        enemySpawner.toKill = 20;


        playerScript.lp = 100;
        playerScript.soulCount = 0;
        playerScript.tearCount = 0;
        playerScript.mediKitCounter = 0;
        playerScript.totalKillCount = 0;
        playerScript.killCount = 0;


        weaponScript.weaponLevel = 1;
        weaponScript.damage = 10;
        weaponScript.timeBetweenFiring = .2f;


        chestCount = 0;
        
    }


    public void AddChest()
    {
        chestCount++;
        if(chestCount == 2)
        {
            PlayerPrefs.SetInt("canArmor", 1);
            playerScript.canArmor = true;
        }
        PlayerPrefs.SetInt("chestCount", chestCount);

    }
}
