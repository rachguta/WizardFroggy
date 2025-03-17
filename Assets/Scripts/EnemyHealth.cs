using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        Debug.Log(" Цербер погиб!");
        GameObject.Destroy(gameObject);

        // Здесь можно добавить анимацию смерти
    }
    public float health { get { return currentHealth; } }


}
