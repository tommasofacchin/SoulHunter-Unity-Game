using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class SoulUI : MonoBehaviour
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
        text.text = "" + player.soulCount;


    }

    private void OnEnable()
    {
        text.text = "" + player.soulCount;
    }
}
