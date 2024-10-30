using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SuperPower : MonoBehaviour
{
    [SerializeField] private Image _superPowerBarSprite;

    [SerializeField] private TMP_Text powText;
    public void UpdatePowBar(float maxPow, float currentPow)
    {

        _superPowerBarSprite.fillAmount = currentPow / maxPow;

        powText.SetText($"{currentPow}/{maxPow}");
    }

}
