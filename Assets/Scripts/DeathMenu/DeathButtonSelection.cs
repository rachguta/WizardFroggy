using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathButtonSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float scaleTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;
    [SerializeField] private DeathButtonManager buttonManager;
    private Vector3 startScale;
    void Start()
    {
        startScale = transform.localScale;
    }


    private IEnumerator ScaleButton(bool startingAnimation)
    {
        Vector3 endScale;
        float elapsedTime = 0f;
        while (elapsedTime < scaleTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (startingAnimation)
            {
                endScale = startScale * scaleAmount;
            }
            else
            {
                endScale = startScale;
            }
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / scaleTime));
            transform.localScale = lerpedScale;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Select
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Deselect
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(ScaleButton(true));
        buttonManager.lastSelected = gameObject;
        //находим индекс последнего выбранного
        for (int i = 0; i < buttonManager.buttons.Length; i++)
        {
            if (buttonManager.buttons[i] == gameObject)
            {
                buttonManager.lastSelectedIndex = i;
                return;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(ScaleButton(false));
    }
}
