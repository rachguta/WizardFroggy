using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private PlayerHealth playerHealth;
    void Start()
    {
        barImage.fillAmount = 1f;
    }

    void Update()
    {
        barImage.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        
    }
}
