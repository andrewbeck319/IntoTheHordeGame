// Script which will render our rock wall based off distance from player: 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptimizationChunckingScript : MonoBehaviour
{
    // Value that we use to base our render distance off of
    [SerializeField]
    private int distanceFromPlayer;

    // Reference to our player
    private GameObject player; 

    // List of our items that we will render or not based off distanceFromPlayer
    public List<ActivatorItem> activatorItems; 


    // Initilizes our player reference, list of items we wish to render and starts our coroutine
    void Start()
    {
        player = GameObject.Find("Player");
        activatorItems = new List<ActivatorItem>();

        StartCoroutine("CheckActivation");
        
    }

    // This coroutine will run constantly while the game is playing removing objects or adding them to 
    // our render list which will either set them active or not active
    IEnumerator CheckActivation()
    {
        // Due to the way lists work we need two as we cannot directly remove the item from the list unti we have a copy of it
        List<ActivatorItem> removeList = new List<ActivatorItem>(); 

        // If we have items to potentially render, we run through this logic 
        if(activatorItems.Count > 0)
        {
            // For every item within our activatorItems list we run this logic
            foreach (ActivatorItem item in activatorItems)
            {
                // If the distance of the object to the player is greater than the value set, we don't render the object
                if (Vector3.Distance(player.transform.position, item.itemPos) > distanceFromPlayer)
                {
                    if(item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(false);
                    }
                }
                // Otherwise we render the object that's close enough to the player
                else
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }
            }
        }

        // We have a slight delay as this helps reduce lag in our coroutine since it'll always be running
        yield return new WaitForSeconds(0.01f);

        // If our remove list has items (items that we don't want to render at the moment) then we remove them from our active list
        if(removeList.Count > 0)
        {
            foreach (ActivatorItem item in removeList)
            {
                activatorItems.Remove(item); 
            }
        }

        // Same as before this delay helps reduce lag
        yield return new WaitForSeconds(0.01f);

        // We create an almost infinite coroutine loop so our items are always being checked while the game is running
        StartCoroutine("CheckActivation");
    }
}

// Assitant class which holds information about the item we are working with 
public class ActivatorItem
{
    public GameObject item;
    public Vector3 itemPos; 

    public ActivatorItem(GameObject itemNew, Vector3 itemPosNew)
    {
        item = itemNew;
        itemPos = itemPosNew;
    }
    
}
