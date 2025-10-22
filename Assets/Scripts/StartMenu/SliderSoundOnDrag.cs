using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderSoundOnDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider slider;
    private AudioSource audioSource;
    void Start()
    {
        slider = GetComponent<Slider>();

        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        audioSource.Play();
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
