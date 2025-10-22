using System.Collections;
using System.Collections.Generic;
using Unified.UniversalBlur.Runtime;
using UnityEngine;

public class OpenHBCongratulations : MonoBehaviour
{
    [SerializeField] private GameObject defaultObjects;
    [SerializeField] private GameObject hbObjects;
    [SerializeField] private Animator dogAnimator;
    [SerializeField] private Animator witchAnimator;
    [SerializeField] private CanvasGroup winningImage;

    private void Update()
    {
        if(EndInputManager.instance.Confirm && winningImage.alpha == 0f)
        {
            ShowHBCongratulations();
        }
    }
    private void ShowHBCongratulations()
    {
        dogAnimator.SetTrigger("HB");
        witchAnimator.SetTrigger("HB");
        defaultObjects.SetActive(false);
        hbObjects.SetActive(true);
    }

    
}
