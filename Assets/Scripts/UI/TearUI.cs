using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TearUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    private PlayerScript player;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    private void FixedUpdate()
    {
        text.text = "" + player.tearCount;
    }
    private void OnEnable()
    {
        text.text = "" + player.tearCount;
    }
}
