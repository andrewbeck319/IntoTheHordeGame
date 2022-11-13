using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private GameObject _interactionArea;
    private InteractionAreaController _interactionAreaController;
    private InteractionTalkController _interactionTalkController;
    
    void Start()
    {
        this._interactionAreaController = this._interactionArea.GetComponent<InteractionAreaController>();
        this._interactionTalkController = this.GetComponent<InteractionTalkController>();
    }

    
    void Update()
    {
        if (this._interactionAreaController.IsPlayerInside && !this._interactionTalkController.isActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                this.GetComponent<InteractionTalkController>().StartTalk();
                GameManager.Instance.UiManager.InteractionEntryPanelController.Hide();
            }
        }
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
