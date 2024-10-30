using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //Camera


    public GameObject settingsPannel;
    public GameObject startMenu;

    public Action OnClick;

    // Start is called before the first frame update
    void Start()
    {
        OnClick += StartGame;
        OnClick += OpenSettings;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene("LightingHDRP");
    }

    public void OpenSettings()
    {
        startMenu.SetActive(false); 
        settingsPannel.SetActive(true);
    }

}
