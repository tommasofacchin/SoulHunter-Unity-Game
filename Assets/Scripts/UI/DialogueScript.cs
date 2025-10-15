using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{

    public List<string> dialogueLines;
    private int index;

    private TMP_Text text;
    private CanvasGroup group;


    public GameObject UI;
    public GameObject background;


    //private GameObject joystick;
    //private GameObject shootJoystick;
    //private CanvasGroup leftHandle;
    //private CanvasGroup rightHandle;


    public int scene;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        //joystick = GameObject.FindGameObjectWithTag("leftJoystick");
        //shootJoystick = GameObject.FindGameObjectWithTag("rightJoystick");
        //leftHandle = joystick.GetComponent<CanvasGroup>();
        //rightHandle = shootJoystick.GetComponent<CanvasGroup>();

        group.alpha = 0;
        background.SetActive(false);
        scene = PlayerPrefs.GetInt("cutscene");
        if ( scene == 0)
        {
            index = 0;
            Invoke("Pause", 1f);
            UI.SetActive(false);
            background.SetActive(true);
            group.alpha = 1;
            Cursor.visible = true;
            text.SetText(dialogueLines[index]);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("cutscene") == 0)
            {
                if (index < dialogueLines.Count - 1)
                {
                    text.SetText(dialogueLines[++index]);
                }
                else
                {
                    group.alpha = 0;
                    Resume();
                    scene = 1;
                    PlayerPrefs.SetInt("cutscene", 1);
                }
            }
            else
            {
                group.alpha = 0;
                Resume();
            }

        }
    }


    private void Pause()
    {

        Time.timeScale = 0f;
        UI.SetActive(false);
        background.SetActive(true);
        group.alpha = 1;
        Cursor.visible = true;
        //leftHandle.alpha = 0;
        //rightHandle.alpha = 0;
    }
    private void Resume()
    {
        Time.timeScale = 1f;
        UI.SetActive(true);
        background.SetActive(false);
        Cursor.visible = false;
        //leftHandle.alpha = 1;
        //rightHandle.alpha = 1;
    }

    public void newDialogue(string newText)
    {
        Pause();
        text.SetText(newText);
    }

}
