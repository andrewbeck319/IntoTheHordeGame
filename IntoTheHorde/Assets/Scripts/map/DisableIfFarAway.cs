// This class is attached to any game object that we wish to render based off player distance
using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    // Create a gameobject which is reference to the gameobject the script is attached to 
    private GameObject itemActivatorObject; 
    // Create a reference to our optimization script which does most of the work
    private OptimizationChunckingScript activationScript; 

    // Start is called before the first frame update
    void Start()
    {
        // Initilize our game object we wish to render as well as our reference to our other script
        itemActivatorObject = GameObject.Find("GameManager"); 
        activationScript = itemActivatorObject.GetComponent<OptimizationChunckingScript>();

        // Need a coroutine here to enable our list to be created before we start adding items to it (race case)
        StartCoroutine("AddToList");
    }

    // Helps prevent race case where objects are in world before list is initilized
    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.01f);

        // Construct our list with items that will be used to render objects in our game
        activationScript.activatorItems.Add(new ActivatorItem(this.gameObject, transform.position));
    }

}
