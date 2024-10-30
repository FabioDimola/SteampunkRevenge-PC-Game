using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject TitlePanel;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowStartMenu());
    }

   IEnumerator ShowStartMenu()
    {
        yield return new WaitForSeconds(4f);
        MenuPanel.SetActive(true);
        TitlePanel.SetActive(false);
    }
}
