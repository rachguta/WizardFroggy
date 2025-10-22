using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ReadLeaflet : MonoBehaviour
{
    public GameObject leaflet;
    [SerializeField] private Animator leafletAnimator;
    [SerializeField] private TextMeshProUGUI textArea;

    [SerializeField] private string invincibilityText;
    [SerializeField] private string parryText;
    [SerializeField] private string projectileText;
    [SerializeField] private string abcText;

    [SerializeField] private GameObject manaBar;

    [SerializeField] private AudioClip leafletSound;
    private void Update()
    {
        if(InputManager.instance.Confirm)
        {
            CloseLeaflet();
        }
    }
    public void ShowLeaflet(int orderNum)
    {
        AudioManager.instance.PlaySoundFXClip(leafletSound, transform, 1f);
        InputManager.PlayerInput.SwitchCurrentActionMap("Reading");
        switch (orderNum)
        {
            case 0:
                textArea.text = invincibilityText;
                manaBar.gameObject.SetActive(true);
                break;
            case 1:
                textArea.text = parryText;
                break;
            case 2:
                textArea.text = projectileText;
                break;
            case 3:
                textArea.text = abcText;
                break;
        }
        leaflet.SetActive(true);
        leafletAnimator.SetTrigger("Open");

    }
    private void CloseLeaflet()
    {
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        leafletAnimator.SetTrigger("Close");
        leaflet.SetActive(false);
    }
}
