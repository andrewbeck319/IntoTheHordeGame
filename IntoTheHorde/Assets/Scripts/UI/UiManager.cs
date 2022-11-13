using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public InteractionEntryPanelController InteractionEntryPanelController;
    public InteractionTalkPanelController InteractionTalkPanelController;
    
    void Start()
    {
        Debug.Log("[OK] UI Manager");
    }
}
