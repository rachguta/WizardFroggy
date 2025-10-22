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
        //���������� ���������� ����� ����� 
        previousInputMap = InputManager.PlayerInput.currentActionMap.name;
        //����������� �� ����� Dialogue
        InputManager.PlayerInput.SwitchCurrentActionMap("Dialogue");
        //������ ������
        currentDialogueIndex = index;
        //�������� ���������� ����
        dialogueElements.SetActive(true);
        //������� ������� � ��������� ����� ��������� ������
        linesQueue.Clear();
        currentLine = dialogues[currentDialogueIndex].dialogueLines[0];
        foreach (DialogueLine diLine in dialogues[currentDialogueIndex].dialogueLines)
        {
            linesQueue.Enqueue(diLine);
        }
        //���������� ���������� ������
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
            //����������� �������� �� ������ ���������
            
            if(currentLine.emotion != "Idle")
            {
                currentLine.person.GetComponent<Animator>().SetBool(currentLine.emotion, true);
                Debug.Log(currentLine.emotion);
            }
            
            //������ ���� �������
            if(currentLine.clip != null)
            {
                dialogueAudioSource.clip = currentLine.clip;
                dialogueAudioSource.Play();
            }
            //������������� ��� 
            nameArea.text = currentLine.personName;
            //������������� ������
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
