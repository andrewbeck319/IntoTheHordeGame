using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;
    public EnemySpawning enemySpawning;

    private void Start()
    {
        enemySpawning = GetComponent<EnemySpawning>();
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

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
