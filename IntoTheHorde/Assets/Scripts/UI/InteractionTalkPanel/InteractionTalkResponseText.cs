using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTalkResponseText : MonoBehaviour
{
    private string text;
    public InteractionElement.Response ResponseData;
    
    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Select()
    {
        this.GetComponent<TextMeshProUGUI>().SetText("> " + text);
    }

    public void Unselect()
    {
        this.GetComponent<TextMeshProUGUI>().SetText(text);
    }

    private void SetText(string text)
    {
        this.text = text;
        this.GetComponent<TextMeshProUGUI>().SetText(text);
    }

    public void Click()
    {
        Debug.Log("Click()");
        Debug.Log(this.ResponseData);
        this.ResponseData.InteractionTalkController.OnTalkResponse(this.ResponseData);
    }

    public void SetResponseData(InteractionElement.Response responseData)
    {
        this.ResponseData = responseData;
        
        this.SetText(responseData.text);
    }
}
