using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] private Image _expBarSprite;
    
    [SerializeField] private TMP_Text expText;
    public void UpdateExpBar (float maxExp, float currentExp)
    {

        _expBarSprite.fillAmount = currentExp / maxExp;
        
        expText.SetText($"{currentExp}/{maxExp}");
    }


}
