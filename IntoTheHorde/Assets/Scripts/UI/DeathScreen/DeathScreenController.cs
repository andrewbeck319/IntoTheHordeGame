using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text stats;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Time.timeScale = 0f;
        updateStats();
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void updateStats()
    {
        stats.SetText("Enemies killed: " + enemyManager.totalKills + "\nTime Survived: " + Time.timeSinceLevelLoad.ToString("N0") + "s\nTotal Gold Collected: " + moneyHandler.totalGold);
    }
}
