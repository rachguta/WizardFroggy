using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f; // Урон снаряда

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у объекта компонент PlayerHealth
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage); // Наносим урон игроку
        }

        // Уничтожаем снаряд при любом столкновении
        Destroy(gameObject);
    }
}
