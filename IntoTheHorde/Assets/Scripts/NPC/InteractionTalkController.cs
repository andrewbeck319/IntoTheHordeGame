using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTalkController : MonoBehaviour
{
    [SerializeField]public List<InteractionElement> Elements = new List<InteractionElement>();

    public int currentElementIndex = -1;
    public int currentPath = 0;

    public bool isActive = false;
    
    
    void Start()
    {
        Debug.Log("InteractionTalkController start " + this.gameObject.name);
        foreach (var InteractionElement in Elements)
        {
            InteractionElement.InteractionTalkController = this;
            InteractionElement.Init();
        }
        Debug.Log("Number of interaction elements = " + this.Elements.Count);
    }

    public void StartTalk()
    {
        this.isActive = true;
        GameManager.Instance.UiManager.InteractionTalkPanelController.Show();
        this.currentElementIndex = -1;
        this.currentPath = 0;
        Debug.Log("Current index before init = " + this.currentElementIndex);
        this.Next();
        FindObjectOfType<CameraController>().GetComponent<CameraController>().PlayAnimation(CameraController.AnimationSelector.NpcTalk);
    }

    public void StopTalk()
    {
        GameManager.Instance.UiManager.InteractionTalkPanelController.Hide();
        this.isActive = false;
        FindObjectOfType<CameraController>().GetComponent<CameraController>().PlayAnimation(CameraController.AnimationSelector.Normal);
    }

    public void Next()
    {
        int newIndex = this.currentElementIndex + 1;
        //Debug.Log("New index init = " + newIndex);
        while (newIndex < this.Elements.Count && !this.Elements[newIndex].AttachedToPaths.Contains(this.currentPath))
        {
            //Debug.Log("newIndex++ from " + newIndex + " to " + newIndex + 1);
            newIndex++;
        }

        if (newIndex == this.Elements.Count)
        {
            // no more elements, end the talk interaction
            //Debug.Log("no more elements, end the talk interaction, index = " + newIndex + ", count = " + this.Elements.Count);
            this.StopTalk();
        }
        else
        {
            //Debug.Log("Set current element = " + newIndex);
            this.currentElementIndex = newIndex;
            this.ShowCurrentElement();
        }
    }

    public void ShowCurrentElement()
    {
        //Debug.Log("Show element " + this.currentElementIndex + " - " + this.Elements[this.currentElementIndex].NpcText);
        // send info to GUI
        GameManager.Instance.UiManager.InteractionTalkPanelController.SetNpcText(this.Elements[this.currentElementIndex].NpcText);

        List<InteractionElement.Response> responses = this.Elements[this.currentElementIndex].Responses;

        GameManager.Instance.UiManager.InteractionTalkPanelController.SetResponses(responses);
    }

    public void OnTalkResponse(InteractionElement.Response responseData)
    {
        //Debug.Log("Clicked: " + responseData.text);
        if (responseData.pathIndex == -1)
        {
            // this response does not affect paths
            //Debug.Log("Does not affect paths");
            
        }
        else
        {
            //Debug.Log("New path = " + responseData.pathIndex);
            this.currentPath = responseData.pathIndex;
        }

        if (responseData.Function != null)
        {
            responseData.Function.Invoke();
        }
        else
        {
            Debug.Log(responseData.Function);
        }
        
        this.Next();
    }
    
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     this.StartTalk();
        // }
    }
}
