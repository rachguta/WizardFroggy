using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [HideInInspector] public bool cutsceneHasEnded = false;
    [SerializeField] private AudioClip introСlip;
    [SerializeField] private PlayableDirector director;
    private RuntimeAnimatorController controller;
    private void Start()
    {
        // Подписываемся на событие остановки
        director.stopped += OnCutsceneStopped;
    }
    public void StartCutScene()
    {
        AudioManager.instance.PlaySoundFXClip(introСlip, transform, 0.8f);
        director.enabled = true;   
    }
    
    private void OnCutsceneStopped(PlayableDirector aDirector)
    {
        cutsceneHasEnded = true;
    }
}
