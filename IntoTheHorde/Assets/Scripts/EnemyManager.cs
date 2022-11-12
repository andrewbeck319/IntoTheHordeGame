using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;
    private void Start()
    {
        enemySpawning = GetComponentInChildren<EnemySpawning>();
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion
    private EnemySpawning enemySpawning;

    public void OnEnemyDestroyed()
    {
        enemySpawning.enemyCount--;
        if (enemySpawning.spawnTime <= 0 && enemySpawning.enemyCount <= 0)
        {
            enemySpawning.spawnerDone = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
