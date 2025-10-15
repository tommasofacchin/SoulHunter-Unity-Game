using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PauseMenu pauseMenu;
    public Animator panel;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void playGame(bool restart)
    {
        panel.SetTrigger("Active");
        if (restart)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("gunAmmo", 300);
            PlayerPrefs.SetInt("shotGunAmmo", 200);
            PlayerPrefs.SetInt("ak47Ammo", 400);
            PlayerPrefs.SetInt("bazookaAmmo", 400);
            PlayerPrefs.SetFloat("lp", 100);
            PlayerPrefs.SetInt("cutscene", 0);

        }

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            pauseMenu.ResumeGame();
        }
        Invoke("LoadSceneGame", .5f);
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        panel.SetTrigger("Active");
        Invoke("LoadSceneMenu", .5f);
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            //pauseMenu.ResumeGame();
        }
    }

    private void LoadSceneGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadSceneMenu()
    {
        SceneManager.LoadScene(0);
    }
}
