using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class StartingGame : MonoBehaviour
{
    [Header("Main Menu Objects")]
    [SerializeField] private CanvasGroup[] mainMenuObjects;

    [Header("History Settings")]
    [SerializeField] private GameObject comics;
    [SerializeField] private CanvasGroup firstImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip leafletSound;

    private bool isTransitioning = false;
    public void StartTheGame()
    {
        if(!isTransitioning)
        {
            StartCoroutine(StartStory());
        }   
    }

    IEnumerator StartStory()
    {
        isTransitioning = true;

        comics.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        foreach(var btn in buttons)
        {
            btn.GetComponent<ButtonSelectionController>().enabled = false;
            btn.GetComponent<Button>().interactable = false;
        }
        yield return HideAllObjects();

        yield return ShowFirstImage(firstImage);

        isTransitioning = false;
    }
    IEnumerator HideAllObjects()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            musicAudioSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            foreach (var obj in mainMenuObjects)
            {
                obj.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var obj in mainMenuObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }
    IEnumerator ShowFirstImage(CanvasGroup image)
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
}
