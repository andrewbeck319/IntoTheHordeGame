using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject Credits;
    public GameObject Cover;
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        ChestInteractable.resetChestCost();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public enum PanelSelector
    {
        Main,
        Settings,
        Credits
    }
    
    public void SetPanel(string panelName)
    {
        if (panelName == "Main")
        {
            StartCoroutine(this.SetPanel(PanelSelector.Main));
            this.GetComponent<SpriteImagesController>().Next();
        }
        if (panelName == "Settings") StartCoroutine(this.SetPanel(PanelSelector.Settings));
        if (panelName == "Credits") StartCoroutine(this.SetPanel(PanelSelector.Credits));
    }
    
    public IEnumerator SetPanel(PanelSelector panel)
    {
        this.Cover.GetComponent<Animator>().SetTrigger("CoverInAndOut");
        
        yield return new WaitForSeconds(0.5f);

        if (this.Settings.activeSelf)
        {
            Debug.Log("PP saved;");
            PlayerPrefs.Save();
        }
        
        this._hideAllPanels();
        if (panel == PanelSelector.Main)
        {
            this.MainMenu.SetActive(true);
        } else if (panel == PanelSelector.Settings)
        {
            this.Settings.SetActive(true);
        } else if (panel == PanelSelector.Credits)
        {
            this.Credits.SetActive(true);
        }
    }
    
    private void _hideAllPanels()
    {
        this.MainMenu.SetActive(false);
        this.Settings.SetActive(false);
        this.Credits.SetActive(false);
    }
}
