using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public GameObject explosionPrefab;
    [SerializeField] private LayerMask enemyLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll
            (transform.position, 4f, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(75f);
                }
            }
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0,0,0));
        }
    }
}
