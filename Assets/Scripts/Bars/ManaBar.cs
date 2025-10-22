using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private PlayerMana mana;
    void Start()
    {
        barImage.fillAmount = 1f;
    }

    void Update()
    {
        barImage.fillAmount = mana.CurrentMana / mana.MaxMana;

    }

}

