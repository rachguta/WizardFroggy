using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;
    private Coroutine burnCoroutine; // Хранит ссылку на корутину "горения"

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Игрок получил {damage} урона. Текущее здоровье: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void StartBurning(float burnDamage, float duration)
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine); // Если персонаж уже горит, сбрасываем таймер
        }

        burnCoroutine = StartCoroutine(BurnEffect(burnDamage, duration));
    }

    private IEnumerator BurnEffect(float burnDamage, float duration)
    {
        Debug.Log(" Игрок загорелся!");
        float elapsed = 0f;

        while (elapsed < duration)
        {
            TakeDamage(burnDamage * Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log(" Горение закончилось.");
        burnCoroutine = null;
    }

    private void Die()
    {
        Debug.Log(" Игрок погиб!");
        GameObject.Destroy(gameObject);

        // Здесь можно добавить анимацию смерти
    }
    public float health { get { return currentHealth; } }
}
