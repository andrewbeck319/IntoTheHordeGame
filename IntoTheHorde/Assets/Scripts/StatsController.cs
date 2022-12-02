using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public float GameTime = 0f;
    public int EnemiesKilled = 0;
    public int TotalMoneyCollected = 0;
    public int TotalDamageDealth = 0;
    public int TotalDamageTaken = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameTime += Time.deltaTime;
    }
}
