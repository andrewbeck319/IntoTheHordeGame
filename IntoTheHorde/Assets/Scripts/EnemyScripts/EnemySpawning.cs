using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject currentPoint;
    public int index;

    public GameObject[] enemies;
    public float minTime;
    public float maxTime;
    public bool canSpawn;
    public float spawnTime;
    public int enemyCount = 0;
    public bool spawnerDone;

    private void Start()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    private void Update()
    {
        if(canSpawn)
        {
            spawnTime -= Time.deltaTime;
            if(spawnTime < 0)
            {
                canSpawn = false;
            }
        }
    }
    public void SpawnEnemy()
    {
        index = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[index];
        float timeBetween = Random.Range(minTime, maxTime);

        if(canSpawn)
        {

            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemyCount++;
        }

        Invoke("SpawnEnemy", timeBetween);
        if(spawnerDone)
        {
            this.enabled = false;
        }
    }
}
