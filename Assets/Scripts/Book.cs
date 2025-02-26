using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Book : MonoBehaviour
{
    public float interactRadius = 2f; // Радиус взаимодействия
    private PlayerController player; // Ссылка на игрока
    [SerializeField] bool isFirstBook;
    [SerializeField] bool isSecondBook;
    private void Update()
    {
        // Проверяем, есть ли игрок в радиусе
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                break; // Нашли игрока, выходим
            }
        }

        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (isFirstBook)
            {
                player.canShoot = true;
                Debug.Log(" Игрок получил способность: стрельба!");
            }
            else if (isSecondBook)
            {
                player.canDash = true;
                Debug.Log(" Игрок получил способности: рывок!");
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
