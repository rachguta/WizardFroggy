using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToOptions : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private Image leaflet;
    [SerializeField] private Sprite optionsLeafletSprite;
    private void Update()
    {
        if(IntroInputManager.instance.CancelInput)
        {
            Back();
        }
    }
    private void Back()
    {
        leaflet.sprite = optionsLeafletSprite;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        options.SetActive(true);
    }
}
