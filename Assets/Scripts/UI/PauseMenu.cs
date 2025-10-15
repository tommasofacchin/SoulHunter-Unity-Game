using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject menu;
    public GameObject background;
    public GameObject UI;
    public GameObject player;
    public GameObject weapon;
    private Shooting shooting;
    private WeaponScript weaponScript;
    private PlayerScript playerScript;



    public bool isPaused;


    private void Start()
    {
        shooting = weapon.GetComponent<Shooting>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerScript.won)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 0f;
                Cursor.visible = true;
            }
        }
    }


    public void PauseGame()
    {
            Time.timeScale = 0f;
            isPaused = true;
            UI.SetActive(false);
            menu.SetActive(true);
            background.SetActive(true);
            Cursor.visible = true;
            PlayerPrefs.SetFloat("playerX", player.transform.position.x);
            PlayerPrefs.SetFloat("playerY", player.transform.position.y);

            PlayerPrefs.SetInt("gunAmmo", shooting.gunAmmo);
            PlayerPrefs.SetInt("shotGunAmmo", shooting.shotGunAmmo);
            PlayerPrefs.SetInt("ak47Ammo", shooting.ak47Ammo);
            PlayerPrefs.SetInt("bazookaAmmo", shooting.bazookaAmmo);

        PlayerPrefs.SetFloat("soulCount", playerScript.soulCount);
            PlayerPrefs.SetFloat("tearCount", playerScript.tearCount);
            PlayerPrefs.SetInt("mediKitCounter", playerScript.mediKitCounter);
            PlayerPrefs.SetFloat("lp", playerScript.lp);
            PlayerPrefs.SetFloat("totalKillCount", playerScript.totalKillCount);
            PlayerPrefs.SetFloat("killCount", playerScript.killCount);     
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UI.SetActive(true);
        menu.SetActive(false);
        background.SetActive(false);
        Cursor.visible = false;
    }

    
}
