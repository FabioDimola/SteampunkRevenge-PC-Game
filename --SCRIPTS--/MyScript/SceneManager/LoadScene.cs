using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LevelPass1"){

            SceneManager.LoadScene(0);
        }
        if(other.gameObject.name == "LevelPass2")
        {
            SceneManager.LoadScene(1);
        }
    }


    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }
}
