using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettingsController : MonoBehaviour
{
    public Button FpsButton;

    public int FpsSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("limit_fps", 0) == 0)
        {
            PlayerPrefs.SetInt("limit_fps", 60);
        }
        this.FpsButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Limit FPS: " + PlayerPrefs.GetInt("limit_fps").ToString());
        Application.targetFrameRate = this.FpsSelected;
    }

    public void OnLimitFpsButtonClick()
    {
        if (this.FpsSelected == 60)
        {
            this.FpsSelected = 120;
        } else if (this.FpsSelected == 120)
        {
            this.FpsSelected = 30;
        }
        else
        {
            this.FpsSelected = 60;
        }
        
        this.FpsButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Limit FPS: " + this.FpsSelected.ToString());
        PlayerPrefs.SetInt("limit_fps", this.FpsSelected);
        Application.targetFrameRate = this.FpsSelected;
    }
}
