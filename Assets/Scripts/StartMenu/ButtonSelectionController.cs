using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonSelectionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;
    [SerializeField] private ButtonSelectionManager buttonSelectionManager;
    private Vector3 startScale;
    private void Start()
    {
        startScale = transform.localScale;
    }
    private IEnumerator ScaleButton(bool startingAnimation)
    {
        Vector3 endScale;
        float elapsedTime = 0f;
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            if(startingAnimation)
            {
                endScale = startScale * scaleAmount;
            }
            else
            {
                endScale = startScale;
            }

            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));
            transform.localScale = lerpedScale;
            yield return null;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(ScaleButton(true));
        buttonSelectionManager.LastSelected = gameObject;
        //Находим индекс
        for (int i = 0; i < buttonSelectionManager.Buttons.Length; i++)
        {
            if (buttonSelectionManager.Buttons[i] == gameObject)
            {
                buttonSelectionManager.LastSelectedIndex = i;
                return;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(ScaleButton(false));
    }

}