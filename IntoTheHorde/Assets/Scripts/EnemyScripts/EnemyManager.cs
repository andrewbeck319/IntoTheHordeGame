using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;
    public EnemySpawning enemySpawning;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private TMP_Text enemyCountText;
    private int killCount = 0;
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
        killCount++;
        moneyHandler.addGold(5);
        enemySpawning.enemyCount--;
        if(killCount >= 5)
        {
            enemySpawning.SpawnChest();
            killCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.SetText("Enemies: " + enemySpawning.enemyCount);
    }
}
