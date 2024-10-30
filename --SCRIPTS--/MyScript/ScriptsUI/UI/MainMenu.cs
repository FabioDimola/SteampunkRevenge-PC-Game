using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneToLoadNG;

    [SerializeField] public GameObject panel;
   // [SerializeField] private string sceneToLoadLG;

    private bool pressed1 = true;
    private int count = 0;

    public static event Action OnClick;

    // Start is called before the first frame update

    private void Awake()
    {
        OnClick += StartGame;
        OnClick += LoadGame;
        panel.SetActive(false);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoadNG);

    }

    public void LoadGame()
    {
        if(count == 0)
        {
            panel.SetActive(true);
            count = 1;
        }
        else
        {
            panel.SetActive(false);
            count = 0;
        }
        
    }

    private void OnDestroy()
    {
        OnClick -= StartGame;
        
    }

    
}
