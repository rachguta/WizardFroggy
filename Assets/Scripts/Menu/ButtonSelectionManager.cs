using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionManager : MonoBehaviour
{
    public static ButtonSelectionManager instance;

    public GameObject[] Buttons;

    public GameObject LastSelected {  get; set; }
    public int LastSelectedIndex { get; set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        //Если нажали вверх
        if (InputManager.instance.NavigationInput.y > 0)
        {
            HandleNextButtonSelection(1);
        }

        //Если нажали вниз
        if (InputManager.instance.NavigationInput.y < 0)
        {
            HandleNextButtonSelection(-1);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(SetSelectedAfterOneFrame());
    }
    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(Buttons[0]); 
    }
    private void HandleNextButtonSelection(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
        {
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, Buttons.Length - 1);
            EventSystem.current.SetSelectedGameObject(Buttons[newIndex]);
        }
    }
}
