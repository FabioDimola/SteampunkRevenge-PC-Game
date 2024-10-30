using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TMP_Text healthText;







    public static Health instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }



    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (maxHealth - currentHealth >= 0)
        {
            _healthBarSprite.fillAmount = currentHealth / maxHealth;
            _healthBarSprite.color = gradient.Evaluate(_healthBarSprite.fillAmount);
            healthText.SetText($"{currentHealth}/{maxHealth}");
        }
    }

  

}
