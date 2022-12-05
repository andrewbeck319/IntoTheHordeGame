using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;
    public EnemySpawning enemySpawning;
    [SerializeField] private MoneyHandler moneyHandler;

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
        moneyHandler.addGold(5);
        enemySpawning.enemyCount--;
        if (enemySpawning.spawnTime <= 0 && enemySpawning.enemyCount <= 0)
        {
            enemySpawning.spawnerDone = true;
            enemySpawning.waitTime = 15f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
