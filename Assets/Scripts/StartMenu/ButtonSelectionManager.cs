using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionManager : MonoBehaviour
{

    public GameObject[] Buttons;
    public GameObject LastSelected {  get; set; }
    public int LastSelectedIndex { get; set; }

    [SerializeField] private bool isMainMenu;
    [SerializeField] private bool isOptionsMenu;
    
    private void Update()
    {
        //Если нажали вверх
        if (IntroInputManager.instance.NavigationInput.y > 0)
        {
            HandleNextButtonSelection(-1);
        }

        //Если нажали вниз
        if (IntroInputManager.instance.NavigationInput.y < 0)
        {
            HandleNextButtonSelection(1);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(SetSelectedAfterOneFrame());
    }
    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        if (isMainMenu)
        {
            EventSystem.current.SetSelectedGameObject(Buttons[0]);
        }
        else if (isOptionsMenu)
        {
            EventSystem.current.SetSelectedGameObject(Buttons[1]);
        }
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
