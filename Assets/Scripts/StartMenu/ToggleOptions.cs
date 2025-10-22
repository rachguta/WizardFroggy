using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ToggleOptions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject[] mainMenuButtons;
    [SerializeField] private GameObject[] optionsButtons;
    [SerializeField] private AudioClip toggleOptionsSound;

    private void Update()
    {
        if(IntroInputManager.instance.CancelInput && optionsMenu.transform.Find("Options").gameObject.activeSelf)
        {
            Close();
        }
    }
    public void Open()
    {
        AudioManager.instance.PlaySoundFXClip(toggleOptionsSound, transform, 1f);
        // 1. Выключаем все кнопки главного меню
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.GetComponent<ButtonSelectionManager>().enabled = false; 
        foreach (var btn in mainMenuButtons)
        {
            btn.GetComponent<ButtonSelectionController>().enabled = false;
            btn.GetComponent<Button>().interactable = false;
        }
        // 2. Включаем кнопки опций 
        options.SetActive(true);
        foreach (var opt in optionsButtons)
        {
            opt.GetComponent<ButtonSelectionController>().enabled = true;
            opt.GetComponent<Button>().interactable = true;
        }
        //3. Открываем меню опций
        optionsMenu.SetActive(true);
        animator.SetTrigger("Open");

    }
    public void Close()
    {
        StartCoroutine(CloseTab());
    }
    public IEnumerator CloseTab()
    {
        Debug.Log("Close");
        AudioManager.instance.PlaySoundFXClip(toggleOptionsSound, transform, 1f);
        // 1. Выключаем кнопки опций 
        EventSystem.current.SetSelectedGameObject(null);
        foreach (var opt in optionsButtons)
        {
            opt.GetComponent<ButtonSelectionController>().enabled = false;
            opt.GetComponent<Button>().interactable = false;
        }

        // 2. Скрываем меню опций
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        options.SetActive(false);
        optionsMenu.SetActive(false);

        // 3. Включаем все кнопки главного меню
        mainMenu.GetComponent<ButtonSelectionManager>().enabled = true;
        foreach (var btn in mainMenuButtons)
        {
            btn.GetComponent<ButtonSelectionController>().enabled = true;
            btn.GetComponent<Button>().interactable = true;
        }
    }
}
