using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Настройки кнопок")]
    public CanvasGroup[] buttons; // Все кнопки меню

    [Header("Настройки истории")]
    public CanvasGroup[] storyImages; // Все картинки истории
    public float fadeDuration = 1f;   // Длительность анимации

    private int currentStoryIndex = -1; // Текущая картинка (-1 = не начато)
    private bool isTransitioning = false;

    private PlayerInputActions playerInputActions;

    private void Start()
    {
        //playerInputActions = new PlayerInputActions();
        //playerInputActions.GameManagment.Enable();
        //playerInputActions.GameManagment.SelectionButton.performed += PressStartButton;
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }
    //private void PressStartButton(InputAction.CallbackContext context)
    //{
    //    if (EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
    //    {
    //        StartCoroutine(HideAllButtonsAndShowStory());

    //    }

    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentStoryIndex == -1) // Если история ещё не начата
            {
                if (EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
                {
                    StartCoroutine(StartStorySequence());
                }
            }
            else if (!isTransitioning && currentStoryIndex < storyImages.Length) // Если история начата и нет активной анимации
            {
                StartCoroutine(ShowNextOrLoadScene());
            }
        }
    }
    IEnumerator StartStorySequence()
    {
        isTransitioning = true;

        // 1. Скрываем все кнопки
        EventSystem.current.SetSelectedGameObject(null);
        yield return HideAllButtons();

        // 2. Показываем первую картинку
        currentStoryIndex = 0;
        yield return ShowImage(storyImages[currentStoryIndex]);

        isTransitioning = false;
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
    }
    IEnumerator ShowNextOrLoadScene()
    {
        isTransitioning = true;


        // 2. Если это не последняя картинка - показываем следующую
        if (currentStoryIndex < storyImages.Length - 1)
        {
            currentStoryIndex++;
            yield return ShowImage(storyImages[currentStoryIndex]);
        }
        else // Если это последняя картинка - загружаем сцену
        {
            SceneManager.LoadScene("SampleScene");
        }

        isTransitioning = false;
    }
    IEnumerator HideAllButtons()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            foreach (var button in buttons)
            {
                button.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}