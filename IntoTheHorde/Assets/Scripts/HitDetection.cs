using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit");

            GameObject enemy = other.gameObject;
            Enemy enemyController = enemy.GetComponent<Enemy>();
            enemyController.TakeDamage();
            //HealthHandler healthHandler = other.GetComponent<HealthHandler>();
            //healthHandler.healthSystem.Damage(10);
        }
        if(other.tag == "Player")
        {
            Debug.Log("Player hit");

            HealthHandler healthHandler = other.GetComponent<HealthHandler>();
            healthHandler.healthSystem.Damage(10);
        }
    }
}
