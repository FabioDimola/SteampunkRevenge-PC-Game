using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance;

    [SerializeField] private TMP_Text TextPaper;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void UpdatePaperText(float maxPaper, float currentPaper)
    {
                  
           TextPaper.SetText($"{maxPaper}/{currentPaper}");
        
    }
}
