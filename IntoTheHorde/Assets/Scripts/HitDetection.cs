using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    // Contains a list of all objects that are potentially hitable within range of weapon collider
    public List<GameObject> hitableObjects; 
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
        if(!hitableObjects.Contains(other.gameObject)){
            hitableObjects.Add(other.gameObject);

        }
        // Code moved to character combat
        // if ((this.transform.parent.parent.tag == "Player") && (other.tag == "Enemy")) //can clean this up and standardize tags
        // {
        //     Debug.Log("Enemy hit");
        //     GameObject enemy = other.gameObject;
        //     Enemy enemyController = enemy.GetComponent<Enemy>();
        //     enemyController.TakeDamage(GetComponentInParent<PlayerStats>());
        // }
        // if(other.tag == "Player")
        // {
        //     Debug.Log("Player hit");
        //     GameObject player = other.gameObject;
        //     PlayerController playerController = player.GetComponent<PlayerController>();
        //     playerController.TakeDamage(GetComponentInParent<EnemyStats>());
        // }
    }

    private void OnTriggerExit(Collider other){
        hitableObjects.Remove(other.gameObject); 

    }
}
