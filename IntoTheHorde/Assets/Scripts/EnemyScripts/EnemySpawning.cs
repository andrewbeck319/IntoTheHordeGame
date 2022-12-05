using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public GameObject currentPoint;
    public int index;

    public GameObject[] enemies;
    public float minTime;
    public float maxTime;
    public bool canSpawn;
    public float spawnTime;
    public float waitTime;
    public int enemyCount = 0;
    public bool spawnerDone;
    public GameObject player;
    public GameObject chest;

    private void Start()
    {
        Invoke("SpawnEnemy", 8f);
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
        else if(spawnerDone)
        {
            waitTime -= Time.deltaTime;
            if(waitTime < 0)
            {
                spawnerDone = false;
                spawnTime = 15f;
                canSpawn = true;
            }
        }
    }
    public void SpawnEnemy()
    {
        //index = Random.Range(0, spawnPoints.Length);
        //currentPoint = spawnPoints[index];
        //Vector3 spawnPosition = getRandomPosition();
        Transform nearestSpawn = getClosestSpawnPoint();
        float timeBetween = Random.Range(minTime, maxTime);

        if(canSpawn)
        {

            Instantiate(enemies[Random.Range(0, enemies.Length)], nearestSpawn.position, Quaternion.identity);
            enemyCount++;
        }

        if(!spawnerDone)
        {
            Invoke("SpawnEnemy", timeBetween);
        }
    }

    //public Vector3 getRandomPosition()
    //{
    //    float _x = Random.Range(-15, 15);
    //    float _z = Random.Range(-15, 15);
    //    float _y = 5f;
    //    Vector3 position = player.transform.TransformPoint(new Vector3(_x, _y, _z));
    //    return position;
    //}

    private Transform getClosestSpawnPoint()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        foreach(Transform t in this.spawnPoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if(dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void SpawnChest()
    {
        Instantiate(chest, player.transform.position, Quaternion.identity);
    }
}
