using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public static LoadGame instance;
    public static ScoreCont scoreC;
   
    public string filePath;

    public Action OnClick;
  

    // Start is called before the first frame update
    void Awake()
    {
        OnClick += OnLoad;
       

        if (instance == null)
        {
            instance = this;
            filePath = Application.persistentDataPath + "/gameData.json";
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
       

    }


    public void OnLoad()
    {
        string jsonString = File.ReadAllText(filePath);
        scoreC = JsonUtility.FromJson<ScoreCont>(jsonString);
        Debug.Log("Load");
    }
}
