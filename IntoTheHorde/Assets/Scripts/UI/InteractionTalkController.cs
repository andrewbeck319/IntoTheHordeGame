using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTalkController : MonoBehaviour
{
    public List<InteractionTalkElement> Elements;

    public int currentElement = 0;
    public int currentPath = 0;
    
    
    void Start()
    {
        
    }

    public void InitTalkElements()
    {
        InteractionTalkElement newInteractionTalkElement;
        // 1st
        newInteractionTalkElement = new InteractionTalkElement(TalkTypeSelector.Sentence);
        newInteractionTalkElement.SetSentence("Hello there!");
        newInteractionTalkElement.SetResponseText("Hi!");
        this.Elements.Add(newInteractionTalkElement);
        
        // 2nd
        newInteractionTalkElement = new InteractionTalkElement(TalkTypeSelector.Sentence);
        newInteractionTalkElement.SetSentence("Maybe I have something for u!");
        this.Elements.Add(newInteractionTalkElement);
        
        // 3rd
        newInteractionTalkElement = new InteractionTalkElement(TalkTypeSelector.Choice);
        newInteractionTalkElement.SetSentence("Do you want a free");
        newInteractionTalkElement.SetResponseText("Hi!");
        this.Elements.Add(newInteractionTalkElement);
    }
    
    void Update()
    {
        
    }
}
