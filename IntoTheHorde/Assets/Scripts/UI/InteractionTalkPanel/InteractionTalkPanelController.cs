using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTalkPanelController : MonoBehaviour
{
    public GameObject UiContainer;
    public InteractionTalkResponseText[] InteractionTalkResponseTexts = new InteractionTalkResponseText[4];
    [SerializeField] private TextMeshProUGUI NpcText;

    private int selectedResponseIndex = 0;
    private int numberOfResponses = 1;

    public bool isActive = false;
    private bool _isInputActive = false;

    public void Show()
    {
        Debug.Log("Show();");
        this.UiContainer.SetActive(true);
        this.isActive = true;

        StartCoroutine(this._activeInput());
    }

    private IEnumerator _activeInput()
    {
        yield return new WaitForSeconds(0.5f);
        this._isInputActive = true;
    }

    public void Hide()
    {
        this.UiContainer.SetActive(false);
        this.isActive = false;
        this._isInputActive = false;
    }

    public void SetNpcText(string text)
    {
        this.NpcText.text = text;
    }

    public void SetResponses(List<InteractionElement.Response> responses)
    {
        this.numberOfResponses = responses.Count;

        for (int i = 0; i < this.InteractionTalkResponseTexts.Length; i++)
        {
            if (i < this.numberOfResponses)
            {
                this.InteractionTalkResponseTexts[i].Show();
                this.InteractionTalkResponseTexts[i].SetResponseData(responses[i]);
            }
            else
            {
                this.InteractionTalkResponseTexts[i].Hide();
            }
        }

        this.SelectResponse(0);
    }

    private void SelectResponse(int responseIndex)
    {
        this.UnselectAllResponses();
        this.selectedResponseIndex = responseIndex;
        this.InteractionTalkResponseTexts[responseIndex].Select();
    }

    private void UnselectAllResponses()
    {
        foreach (var response in this.InteractionTalkResponseTexts)
        {
            response.Unselect();
        }
    }

    private void ConfirmResponse(int responseIndex)
    {
        this.InteractionTalkResponseTexts[responseIndex].Click();
    }

    private void Update()
    {
        if (this._isInputActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.selectedResponseIndex--;
                if (this.selectedResponseIndex < 0) this.selectedResponseIndex = this.numberOfResponses - 1;
                this.SelectResponse(this.selectedResponseIndex);
            } else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.selectedResponseIndex++;
                if (this.selectedResponseIndex >= this.numberOfResponses) this.selectedResponseIndex = 0;
                this.SelectResponse(this.selectedResponseIndex);
            } else if (Input.GetKeyDown(KeyCode.Return))
            {
                this.ConfirmResponse(this.selectedResponseIndex);
            }
        }
    }
}
