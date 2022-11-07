using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawning enemySpawning;

    // Make sure this gets called when player attacks enemy
    public void TakeDamage()
    {
        Destroy(gameObject);
        // May cause problems with multiple spawners
        enemySpawning = FindObjectOfType<EnemySpawning>();
        enemySpawning.enemyCount--;
        if(enemySpawning.spawnTime <= 0 && enemySpawning.enemyCount <= 0)
        {
            enemySpawning.spawnerDone = true;
        }
    }
}
