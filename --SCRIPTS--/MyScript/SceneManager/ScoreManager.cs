using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public ScoreCont scoreC;
    public int localPoint;
    public string filePath;


    public static int saved;
    public Action OnClick;
    public Button buttonSave;

    public GameObject successPanel;
    public GameObject pausePanel;
    public GameObject savePannel;

    // Start is called before the first frame update
    void Awake()
    {
        OnClick += OnLoad;
        OnClick += OnSave;
        OnClick += CloseSuccessPanel;

        //buttonSave.gameObject.SetActive(false);
        if(instance == null)
        {
            instance = this;
            filePath = Application.persistentDataPath + "/gameData.json"; //percorso file di salvataggio
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }
        
    }

    // Update is called once per frame
 /*   void Update()
    {
        if (scoreC.bonus == 5)
        {
            buttonSave.gameObject.SetActive(true);
             
        }
        
    } */

    public void OnSave() //salvataggio dati di gioco
    {
        string jsonString = JsonUtility.ToJson(scoreC, true);
        File.WriteAllText(filePath, jsonString);
        saved++;
        successPanel.SetActive(true);
        
    }

    public void OnLoad()
    {
        string jsonString = File.ReadAllText(filePath);
        scoreC = JsonUtility.FromJson<ScoreCont>(jsonString);
        Debug.Log("Load");
    }

    public void CloseSuccessPanel() // chiusura pannello messaggio di successo
    {
        successPanel.SetActive(false);
        savePannel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
