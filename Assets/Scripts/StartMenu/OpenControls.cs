using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenControls : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject keyboardControls;
    [SerializeField] private GameObject gamepadControls;

    [SerializeField] private Image leaflet;
    [SerializeField] private Sprite controlsLeafletSprite;

    public void OpenKeyboardControls()
    {
        options.SetActive(false);
        leaflet.sprite = controlsLeafletSprite;
        keyboardControls.SetActive(true);
    }
    public void OpenGamepadControls()
    {
        options.SetActive(false);
        leaflet.sprite = controlsLeafletSprite;
        gamepadControls.SetActive(true);
    }

        

}
