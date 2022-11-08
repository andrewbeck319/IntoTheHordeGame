using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTalkElement
{
    public InteractionTalkElement(TalkTypeSelector talkType)
    {
        this.TalkType = talkType;
    }

    public InteractionTalkElement(string sentence, string responseText)
    {
        this.TalkType = TalkTypeSelector.Sentence;
        this.ResponseText = responseText;
    }
    
    public InteractionTalkElement(string sentence)
    {
        this.TalkType = TalkTypeSelector.Sentence;
        this.ResponseText = "OK!";
    }
    
    public InteractionTalkElement(string sentence, List<string> responsesText)
    {
        this.TalkType = TalkTypeSelector.Choice;
        this.Choices = responsesText;
    }

    public TalkTypeSelector TalkType;
    public string Sentence;
    public string ResponseText = "OK";
    public List<string> Choices;
    public List<int> LimitedToPaths = new List<int>();

    public void SetSentence(string sentence)
    {
        this.Sentence = sentence;
    }

    public void SetResponseText(string text)
    {
        this.ResponseText = text;
    }

    public void SetChoices(List<string> choices)
    {
        this.Choices = choices;
    }

    public void LimitToPaths(List<int> paths)
    {
        this.LimitedToPaths = paths;
    }
}
