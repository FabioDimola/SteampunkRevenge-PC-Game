using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PauseScript : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject mapPanel;
    public GameObject settingsPanel;
    public Action onClick;
    public GameObject savePanel;
    public GameObject exitPanel;
    int count = 0;
    public GameObject infoPanel;
    public GameObject infoPanel2;
    public GameObject infoPanel3;
    public GameObject infoPanel4;
    private bool isPaused;
    public GameObject abilityPanel;
    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        onClick += CloseMenu;
        onClick += OpenMap;
        onClick += OpenSettings;
        onClick += Died;
        onClick += SaveBTN;
        onClick += CloseSavePanel;
        onClick += CloseInfo;
        onClick += OpenAbility;
        
        onClick += OpenInvenotry;
       
        
        isPaused = false;
    }





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused; 
            Pause();
        }
    }


    public void Pause()
    {
      
            pausePanel.SetActive(true);
        inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
        
    }

    public void CloseMenu()
    {

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenMap()
    {
        mapPanel.SetActive(true);
        settingsPanel.SetActive(false);
        abilityPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    public void OpenSettings()
    {
        mapPanel.SetActive(false);
        settingsPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }

    //Dead
    public void Died() //in caso di morte del giocatore riappare il menu di partenza
    {
        SceneManager.LoadScene("MenuStart");
    }

    public void SaveBTN() //bottone di salvataggio
    {
        if (count == 0)
        {
            savePanel.SetActive(true);
            count = 1;
        }
        else
        {
            savePanel.SetActive(false);
            count = 0;
        }
    }

    public void CloseSavePanel()
    {
        savePanel.SetActive(false);
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
        infoPanel2.SetActive(false);
        infoPanel3.SetActive(false);
        infoPanel4.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        pausePanel.SetActive(false);
        exitPanel.SetActive(false );
        SceneManager.LoadScene("MenuStart");
    }
    public void OpenExitPannel()
    {
        exitPanel.SetActive(true);
    }

    public void CloseExitPannel()
    {
        
        exitPanel.SetActive(false);
    }

    public void OpenInvenotry()
    {
        inventoryPanel.SetActive(true);
        mapPanel.SetActive(false);
        settingsPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }

   

    public void OpenAbility()
    {
        abilityPanel.SetActive(true);
        mapPanel.SetActive(false);
        settingsPanel.SetActive(false);
        inventoryPanel.SetActive(false);    
    }

}
