using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeathButtonManager : MonoBehaviour
{

    public GameObject[] buttons;

    public GameObject lastSelected { get; set; }

    public int lastSelectedIndex { get; set; }

    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        //Если двигаемся вправо, выбираем следующий
        if (InputManager.instance.UINavigationInput.x > 0)
        {
            HandleNextButtonSelection(1);
        }

        //Если двигаемся влево, выбираем предыдущий
        if (InputManager.instance.UINavigationInput.x < 0)
        {
            HandleNextButtonSelection(-1);  
        }
    }
    private void OnEnable()
    {
        audioSource.Play();
        StartCoroutine(SetSelectedAfterOneFrame());

    }
    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }
    private void HandleNextButtonSelection(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
        {
            int newIndex = lastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, buttons.Length - 1);
            EventSystem.current.SetSelectedGameObject(buttons[newIndex]);
        }
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }
}
