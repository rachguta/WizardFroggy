using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
[System.Serializable]
public class DialogueLine
{
    public GameObject person;
    public string personName;
    public Sprite icon;
    public AudioClip clip;
    public string text;
    public string emotion;
}
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    
    [HideInInspector] public bool isPassed;

}
