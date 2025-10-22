using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [HideInInspector] public bool cutsceneHasEnded = false;
    [SerializeField] private AudioClip intro�lip;
    [SerializeField] private PlayableDirector director;
    private RuntimeAnimatorController controller;
    private void Start()
    {
        // ������������� �� ������� ���������
        director.stopped += OnCutsceneStopped;
    }
    public void StartCutScene()
    {
        AudioManager.instance.PlaySoundFXClip(intro�lip, transform, 0.8f);
        director.enabled = true;   
    }
    
    private void OnCutsceneStopped(PlayableDirector aDirector)
    {
        cutsceneHasEnded = true;
    }
}
