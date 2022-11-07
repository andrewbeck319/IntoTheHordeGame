using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionEntryPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);   
    }
}
