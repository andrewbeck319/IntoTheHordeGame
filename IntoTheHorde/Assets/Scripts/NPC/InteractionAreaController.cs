using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAreaController : MonoBehaviour
{
    private NpcController NpcController;
    public bool IsPlayerInside = false;
    // Start is called before the first frame update
    void Start()
    {
        this.NpcController = this.GetComponentInParent<NpcController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        this.NpcController.OnPlayerEnter();
        this.IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited");
        this.NpcController.OnPlayerExit();
        this.IsPlayerInside = false;
    }
}
