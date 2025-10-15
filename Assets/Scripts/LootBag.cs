using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject SoulPrefab;
    public GameObject TearPrefab;
    public List<Loot> lootList = new List<Loot>();
    public float dropForce;

    List<Loot> GetDroppedItems()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItem = new List<Loot>();

        foreach(Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItem.Add(item);
                return possibleItem;
            }
        }
        return null;
    }



    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedItem = GetDroppedItems();
        if(droppedItem != null)
        {

            GameObject lootObject = Instantiate(SoulPrefab, spawnPosition, Quaternion.identity);

            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

            int randomNumber = Random.Range(1, 11);

            for(int i = 0; i < randomNumber; i++)
            {
                lootObject = Instantiate(TearPrefab, spawnPosition, Quaternion.identity);
                dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

            }

        }
    }
}
