using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Completion : MonoBehaviour
{
    [SerializeField] private InputActionReference click;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject winningImage;
    private float timer = 0f;
    private const string INTRO_SCENE = "Intro";
    private void OnEnable()
    {
        click.action.Enable();
        click.action.performed += Ending;
    }
    private void OnDisable()
    {
        click.action.Disable();
        click.action.performed -= Ending;
    }
    private void Update()
    {
        if(timer < 1f)
        {
            timer += Time.deltaTime;
        }
    }

    public void Ending(InputAction.CallbackContext context)
    {
       
        if (timer >= 1f && winningImage.activeSelf)
        {
            StartCoroutine(HideWinningImage());
        }
        else if(!winningImage.activeSelf)
        {
            LoadIntroScene();
        }

    }
    private IEnumerator HideWinningImage()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            winningImage.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        winningImage.GetComponent<CanvasGroup>().alpha = 0f;
        winningImage.SetActive(false);

    }
    private void LoadIntroScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(INTRO_SCENE);
    }


}
