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
    [SerializeField] private PlayerStats playerStats;
    private HealthSystem healthSystem;
    private int killCount = 0;
    private int requiredKills = 5;
    private void Start()
    {
        enemySpawning = GetComponent<EnemySpawning>();
        HealthHandler healthHandler = playerStats.gameObject.GetComponent<HealthHandler>();
        healthSystem = healthHandler.healthSystem;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void OnEnemyDestroyed()
    {
        healOnKill();
        killCount++;
        moneyHandler.addGold(5);
        enemySpawning.enemyCount--;
        if(killCount >= requiredKills)
        {
            enemySpawning.SpawnChest();
            killCount = 0;
            requiredKills++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.SetText("Enemies: " + enemySpawning.enemyCount);
    }

    private void healOnKill()
    {
        healthSystem.Heal(playerStats.healthOnKill);
        playerStats.maxHealth = healthSystem.GetMaxHealth();
        playerStats.currentHealth = healthSystem.GetHealth();
        Debug.Log("Heal on kill for: " + playerStats.healthOnKill); 
    }
}
