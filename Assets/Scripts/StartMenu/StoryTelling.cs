using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class StoryTelling : MonoBehaviour
{
    [SerializeField] private InputActionReference click;
    [SerializeField] private CanvasGroup[] storyImages;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private AudioClip leafletSound;

    [SerializeField] private string MAIN_SCENE = "MainScene";
    private int currentIndex = 0;
    private void OnEnable()
    {
        click.action.Enable();
        click.action.performed += StartStory;

    }
    private void OnDisable()
    {
        click.action.Disable();
        click.action.performed -= StartStory;
    }
    private void StartStory(InputAction.CallbackContext context)
    {
        if (storyImages[currentIndex].alpha == 1)   
        {
            if(currentIndex < storyImages.Length - 1)
            {
                StartCoroutine(ShowImage(storyImages[++currentIndex]));
            }
            else if (currentIndex == storyImages.Length - 1)
            {
                LoadNextScene();
            }
        }
        
    }
    IEnumerator ShowImage(CanvasGroup image)
    {
        image.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            image.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.alpha = 1f;
        AudioManager.instance.PlaySoundFXClip(leafletSound, transform, 0.3f);

    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(MAIN_SCENE);
    }
    

}
