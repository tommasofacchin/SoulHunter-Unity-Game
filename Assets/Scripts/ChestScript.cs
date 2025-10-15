using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChestScript : MonoBehaviour
{

    private ButtonE button;
    private WeaponScript weaponScript;

    public GameObject buttonE;
    public GameObject weapon;

    public Animator animator;
    public AudioSource openingSound;

    public GameObject save;
    private SaveSystem saveSystem;

    public bool inRange;
    public bool isClick;
    public bool haveWeapon;

    public GameObject dialogue;
    private DialogueScript dialogueScript;

    public string text;

    private void Awake()
    {
        button = buttonE.GetComponent<ButtonE>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        animator.GetComponent<Animator>(); 
        openingSound = GetComponent<AudioSource>();
        saveSystem = save.GetComponent<SaveSystem>();
        dialogueScript = dialogue.GetComponent<DialogueScript>();
        isClick = false;
    }

    private void Update()
    {

        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (haveWeapon)
            {
                weaponScript.WeaponLevelUp();
            }
            animator.SetBool("isOpened", true);
            openingSound.Play();
            saveSystem.AddChest();
            Destroy(buttonE);
            switch (weaponScript.weaponLevel)
            {
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            dialogueScript.newDialogue(text);
        }
    }

    public void isOpened()
    {
        animator.SetBool("isOpened", true);
        Destroy(buttonE);
    }


}
