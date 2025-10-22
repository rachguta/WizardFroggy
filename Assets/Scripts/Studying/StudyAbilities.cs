using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyAbilities : MonoBehaviour
{
    public GameObject book;

    [SerializeField] private GameObject hint;
    [SerializeField] private AudioClip bookFalling;



    public void InitiateBook()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        book.SetActive(true);
        AudioManager.instance.PlaySoundFXClip(bookFalling, transform, 1f);
        if(book.GetComponent<Book>().bookType == BookType.First)
        {
            yield return new WaitUntil(() => DialogueManager.instance.dialogues[1].isPassed);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        hint.SetActive(true);

    }
}
