// This class is attached to any game object that we wish to render based off player distance
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceManagerScript : MonoBehaviour
{
    List<GameObject> managedObjects; 

    public int distanceFromPlayer; 

    public Transform player; 
    // Start is called before the first frame update
    void Start()
    {
       managedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HideObject")); 
    }

    void Update()
    {
        foreach(GameObject obj in managedObjects)
        {
            if((obj.transform.position.x - player.position.x) * (obj.transform.position.x - player.position.x)  
                + (obj.transform.position.y - player.position.y) * (obj.transform.position.y - player.position.y) > distanceFromPlayer)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true); 
            }
        }
    }

}
