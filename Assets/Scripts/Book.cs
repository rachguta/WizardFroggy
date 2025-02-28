using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Book : MonoBehaviour
{
    public float interactRadius = 1.5f; // Радиус взаимодействия
    private PlayerController player; // Ссылка на игрока
    [SerializeField] bool isFirstBook;
    [SerializeField] bool isSecondBook;
    [SerializeField] bool isThirdBook;
    private void Update()
    {
        // Проверяем, есть ли игрок в радиусе
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                break; 
            }
        }

        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (isFirstBook)
            {
                player.canDash = true;
                Debug.Log(" Игрок получил способности: рывок!");
                
            }
            else if (isSecondBook)
            {
                Parry parry = player.GetComponentInChildren<Parry>();
                if (parry != null)
                {
                    parry.canParry = true;
                    Debug.Log("Игрок получил способность: парирование!");
                }
                else
                {
                    Debug.LogWarning(" Parry не найден внутри Player!");
                }

            }
            else if (isThirdBook)
            {
                player.canShoot = true;
                Debug.Log(" Игрок получил способность: стрельба!");
                Debug.Log(" Игра началась!");

            }

            Destroy(gameObject); // Удаляем книгу после использования
        }
    }
    
    // Показываем радиус в редакторе
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
