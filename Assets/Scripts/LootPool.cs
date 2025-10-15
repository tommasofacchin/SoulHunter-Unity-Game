using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootPool : MonoBehaviour
{

    public static LootPool instance;

    private List<GameObject> tears = new List<GameObject>();
    private List<GameObject> souls = new List<GameObject>();
    private int amount1 = 100;
    private int amount2 = 20;

    [SerializeField] private GameObject tearPrefab;
    [SerializeField] private GameObject soulPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amount1; i++)
        {

            GameObject obj = Instantiate(tearPrefab);
            obj.SetActive(false);
            tears.Add(obj);
        }

        for (int i = 0; i < amount2; i++)
        {

            GameObject obj = Instantiate(soulPrefab);
            obj.SetActive(false);
            souls.Add(obj);
        }
    }

    public GameObject GetSouls()
    {
        for (int i = 0; i < souls.Count; i++)
        {
            if (!souls[i].activeInHierarchy)
            {
                return souls[i];
            }
        }

        return null;
    }

    public GameObject GetTears()
    {
        for (int i = 0; i < tears.Count; i++)
        {
            if (!tears[i].activeInHierarchy)
            {
                return tears[i];
            }
        }

        return null;
    }
}
