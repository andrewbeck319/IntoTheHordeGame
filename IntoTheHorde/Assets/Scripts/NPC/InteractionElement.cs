using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InteractionElement
{
    [Serializable]
    public class Response
    {
        public string text;
        public int pathIndex;
        [NonSerialized] public InteractionTalkController InteractionTalkController;
        public UnityEvent Function;
        
        public Response(InteractionTalkController interactionTalkController, string text, int pathIndex)
        {
            this.text = text;
            this.pathIndex = pathIndex;
            this.InteractionTalkController = interactionTalkController;
        }
    }

    public string NpcText;
    public List<Response> Responses = new List<Response>();
    
    public List<int> AttachedToPaths = new List<int>();
    [NonSerialized] public InteractionTalkController InteractionTalkController;
    [NonSerialized] public TalkTypeSelector TalkType;

    public void Init()
    {
        Debug.Log("CONSTRUCTOR AAA");
        foreach (Response response in this.Responses)
        {
            response.InteractionTalkController = this.InteractionTalkController;
        }
        
        if (this.Responses.Count == 0)
        {
            this.Responses.Add(new Response(this.InteractionTalkController, "OK!", -1));
        }
        
        if (this.Responses.Count == 1)
        {
            this.TalkType = TalkTypeSelector.Sentence;
        }
        else
        {
            this.TalkType = TalkTypeSelector.Choice;
        }
    }
}
