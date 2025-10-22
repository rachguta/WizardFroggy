using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
    {
    [SerializeField] private GameObject menu;
   
    [SerializeField] private AudioClip togglePauseSound;
    [SerializeField] private AudioSource pauseAudioSource;
    [SerializeField] private GameObject deathMenu;
    private string previousInputActionMap;
    private const string LOAD_SCENE = "Intro";
    private void Start()
    {
        pauseAudioSource.ignoreListenerPause = true;
    }

    private void Update()
    {
        if ((InputManager.instance.OpenPauseMenuInput || InputManager.instance.OpenPauseFromDialogue || InputManager.instance.OpenPauseFromReading || InputManager.instance.OpenPauseFromCutscene))
        {
            previousInputActionMap = InputManager.PlayerInput.currentActionMap.name;
            TogglePause();
        }
        else if(InputManager.instance.UIClosePauseMenuInput && !deathMenu.activeSelf)
        {
            TogglePause();
        }
       
    }
    public void TogglePause()
    {
        bool isPaused = menu.activeSelf;
       
        menu.SetActive(!isPaused);
        
        if (isPaused)
        {
            InputManager.PlayerInput.SwitchCurrentActionMap(previousInputActionMap);
            Time.timeScale = 1f;
            AudioListener.pause = false;
            
        }
        else
        {
            pauseAudioSource.Play();
            InputManager.PlayerInput.SwitchCurrentActionMap("UI");
            AudioListener.pause = true;
            
            Time.timeScale = 0f;
        }

        
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(LOAD_SCENE);

    }



}
