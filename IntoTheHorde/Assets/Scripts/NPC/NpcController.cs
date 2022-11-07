using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private GameObject _interactionArea;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnPlayerEnter()
    {
        Debug.Log("Player entered");
        GameManager.Instance.UiManager.InteractionEntryPanelController.Show();
    }

    public void OnPlayerExit()
    {
        Debug.Log("Player exited");
        GameManager.Instance.UiManager.InteractionEntryPanelController.Hide();
    }
}
