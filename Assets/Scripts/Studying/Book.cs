using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BookType
{
    First,
    Second,
    Third,
    ABC
}

public class Book : MonoBehaviour
{
    public BookType bookType;
    private PlayerController playerController;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private float interactRadius = 1.5f;
    [SerializeField] private ReadLeaflet readLeaflet; 

    private void Update()
    {
        if(InputManager.instance.InteractionInput)
        {
            GetAbility();
        }
        
    }
    public void GetAbility()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (Collider2D collider in colliders)
        {
            playerController = collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                break;
            }
        }
        if (playerController != null)
        {
            if (bookType == BookType.First && dialogueManager.dialogues[1].isPassed)
            {
                StartCoroutine(Reading(0));

            }

            else if (bookType == BookType.Second)
            {
                StartCoroutine(Reading(1));
            }
            else if (bookType == BookType.Third)
            {
                StartCoroutine(Reading(2));
            }
            else if (bookType == BookType.ABC)
            {
                StartCoroutine(Reading(3));
            }
            
        }
    }
    private IEnumerator Reading(int numOrder)
    {
        readLeaflet.ShowLeaflet(numOrder);
        yield return new WaitUntil(() => readLeaflet.leaflet.activeSelf == false);
        switch (numOrder)
        {
            case 0:
                playerController.canBeInvincible = true;
                Destroy(gameObject);
                break;
            case 1:
                playerController.canParry = true;
                Destroy(gameObject);
                break;
            case 2:
                playerController.canShoot = true;
                Destroy(gameObject);
                break;
            case 3:
                playerController.hasABC = true;
                Destroy(gameObject);
                break;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

    }
}
