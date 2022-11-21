using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    // Contains a list of all objects that are potentially hitable within range of weapon collider
    
    // producer consumer problem, as of now, dead objects don't ever leave producer list because when an object dies, ontriggerexit never runs. additionally, we
    // need to have a consumer as since OnTrigger() functions are continuous i.e. on interrupts, the memory is volatile, cannot be cached, and is most likely invalid by the 
    //time a charactercombat script uses it, so we copy to a consuming object on every discret timestep, every frame. this is a temporary solution. Could fully bandaid it by
    //having a death event.

    public List<GameObject> hitableObjects; //producer
    public List<GameObject> hitableObjectsConsumer; //consumer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitableObjectsConsumer = hitableObjects.ToList();
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
