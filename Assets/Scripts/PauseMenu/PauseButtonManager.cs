using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PauseButtonManager : MonoBehaviour
{
   

    public GameObject[] buttons;

    public GameObject lastSelected {  get; set; }

    public int lastSelectedIndex { get; set; }

    private void Update()
    {
        //Если двигаемся вниз, выбираем предыдущий
        if (InputManager.instance.UINavigationInput.y > 0)
        {
            HandleNextButtonSelection(-1);
        }

        //Если двигаемся вверх, выбираем следующий
        if (InputManager.instance.UINavigationInput.y < 0)
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
}
