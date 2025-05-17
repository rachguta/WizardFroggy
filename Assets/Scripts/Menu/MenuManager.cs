using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("��������� ������")]
    public CanvasGroup[] buttons; // ��� ������ ����

    [Header("��������� �������")]
    public CanvasGroup[] storyImages; // ��� �������� �������
    public float fadeDuration = 1f;   // ������������ ��������

    private int currentStoryIndex = -1; // ������� �������� (-1 = �� ������)
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
            if (currentStoryIndex == -1) // ���� ������� ��� �� ������
            {
                if (EventSystem.current.currentSelectedGameObject == buttons[0].gameObject)
                {
                    StartCoroutine(StartStorySequence());
                }
            }
            else if (!isTransitioning && currentStoryIndex < storyImages.Length) // ���� ������� ������ � ��� �������� ��������
            {
                StartCoroutine(ShowNextOrLoadScene());
            }
        }
    }
    IEnumerator StartStorySequence()
    {
        isTransitioning = true;

        // 1. �������� ��� ������
        EventSystem.current.SetSelectedGameObject(null);
        yield return HideAllButtons();

        // 2. ���������� ������ ��������
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


        // 2. ���� ��� �� ��������� �������� - ���������� ���������
        if (currentStoryIndex < storyImages.Length - 1)
        {
            currentStoryIndex++;
            yield return ShowImage(storyImages[currentStoryIndex]);
        }
        else // ���� ��� ��������� �������� - ��������� �����
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