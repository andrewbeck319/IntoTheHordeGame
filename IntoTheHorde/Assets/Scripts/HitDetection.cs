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
        if ((this.transform.parent.parent.tag == "Player") && (other.tag == "Enemy")) //can clean this up and standardize tags
        {
            Debug.Log("Enemy hit");
            GameObject enemy = other.gameObject;
            Enemy enemyController = enemy.GetComponent<Enemy>();
            enemyController.TakeDamage(GetComponentInParent<PlayerStats>());
        }
        if(other.tag == "Player")
        {
            Debug.Log("Player hit");
            GameObject player = other.gameObject;
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.TakeDamage(GetComponentInParent<EnemyStats>());
        }
    }
}
