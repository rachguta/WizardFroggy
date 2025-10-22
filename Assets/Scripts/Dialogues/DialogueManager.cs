using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private GameObject dialogueElements;
    [SerializeField] private TextMeshProUGUI nameArea;
    [SerializeField] private TextMeshProUGUI dialogueArea;
    [SerializeField] private float textSpeed;
    
    [SerializeField] private Image iconImage;
    [SerializeField] private AudioSource dialogueAudioSource;
    public List<Dialogue> dialogues = new List<Dialogue>();
    private Queue<DialogueLine> linesQueue = new Queue<DialogueLine>();
    private DialogueLine currentLine;
    private int currentDialogueIndex;
    private string previousInputMap;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void StartDialogue(int index)
    {
        //Записываем предыдущую карту ввода 
        previousInputMap = InputManager.PlayerInput.currentActionMap.name;
        //Переключаем на карту Dialogue
        InputManager.PlayerInput.SwitchCurrentActionMap("Dialogue");
        //Меняем индекс
        currentDialogueIndex = index;
        //Включаем диалоговое окно
        dialogueElements.SetActive(true);
        //Очищаем очередь и добавляем новые дилоговые строки
        linesQueue.Clear();
        currentLine = dialogues[currentDialogueIndex].dialogueLines[0];
        foreach (DialogueLine diLine in dialogues[currentDialogueIndex].dialogueLines)
        {
            linesQueue.Enqueue(diLine);
        }
        //Показываем диалоговую строку
        DisplayNextDialogueLine();
    }
    private void Update()
    {
        if(InputManager.instance.SwitchDialogueLine)
        {
            SwitchLine();
        }
    }
    private void DisplayNextDialogueLine()
    {

        if (linesQueue.Count != 0)
        {
            currentLine = linesQueue.Dequeue();
            //Переключаем анимацию на нужное состояние
            
            if(currentLine.emotion != "Idle")
            {
                currentLine.person.GetComponent<Animator>().SetBool(currentLine.emotion, true);
                Debug.Log(currentLine.emotion);
            }
            
            //Меняем звук диалога
            if(currentLine.clip != null)
            {
                dialogueAudioSource.clip = currentLine.clip;
                dialogueAudioSource.Play();
            }
            //Устанавливаем имя 
            nameArea.text = currentLine.personName;
            //Устанавливаем иконку
            iconImage.sprite = currentLine.icon;
            StartCoroutine(TypeSentence(currentLine));
        }
        else
        {
            dialogues[currentDialogueIndex].isPassed = true;
            dialogueElements.SetActive(false);
            InputManager.PlayerInput.SwitchCurrentActionMap(previousInputMap);
        }
    }
    IEnumerator TypeSentence(DialogueLine diLine)
    {
        dialogueArea.text = string.Empty;
        foreach(char let in diLine.text.ToCharArray())
        {
            dialogueArea.text += let;
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueAudioSource.Stop();
    }
    
    public void SwitchLine()
    {
        if(dialogueArea.text == currentLine.text)
        {
            if(currentLine.emotion != "Idle")
            {
                currentLine.person.GetComponent<Animator>().SetBool(currentLine.emotion, false);
            }
            DisplayNextDialogueLine();
        }
        else
        {
            dialogueAudioSource.Stop();
            StopAllCoroutines();
            dialogueArea.text = currentLine.text;
        }
    }
   
}
