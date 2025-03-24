using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;
    private GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Цербер получил {damage} урона. Текущее здоровье: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameManager.GameOver();
        GameObject.Destroy(gameObject);

    }
    public float health { get { return currentHealth; } }


}
