using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTalkController : MonoBehaviour
{
    public List<InteractionTalkElement> Elements = new List<InteractionTalkElement>();

    public int currentElementIndex = 0;
    public int currentPath = 0;
    
    
    void Start()
    {
        this.InitTalkElements();
    }

    public void InitTalkElements()
    {
        InteractionTalkElement newInteractionTalkElement;
        // 1st
        newInteractionTalkElement = new InteractionTalkElement("Hello there!", "Hi");
        this.Elements.Add(newInteractionTalkElement);
        
        // 2nd
        newInteractionTalkElement = new InteractionTalkElement("Maybe I have something for u!");
        this.Elements.Add(newInteractionTalkElement);
        
        // 3rd
        newInteractionTalkElement = new InteractionTalkElement("What's your decision?", 
            new List<string>(){"A stick", "Nothing"});
        this.Elements.Add(newInteractionTalkElement);
    }

    public void EvaluateResponse(InteractionTalkElement interactionTalkElement)
    {
        if (interactionTalkElement.TalkType == TalkTypeSelector.Sentence)
        {
            // just confirmation, proceed to next element
        } else if (interactionTalkElement.TalkType == TalkTypeSelector.Choice)
        {
            // choice, set the appropiate path
        }
    }

    public void Next()
    {
        int newIndex = this.currentElementIndex + 1;
        while (newIndex < this.Elements.Count && this.Elements[newIndex].LimitedToPaths.Contains(this.currentPath))
        {
            newIndex++;
        }

        if (newIndex == this.Elements.Count)
        {
            // no more elements, end the talk interaction
            Debug.Log("no more elements, end the talk interaction");
        }
        else
        {
            this.currentElementIndex = newIndex;
            this.ShowCurrentElement();
        }
    }

    public void ShowCurrentElement()
    {
        Debug.Log("Show element " + this.currentElementIndex);
        // send info to GUI
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.Next();
        }
    }
}
