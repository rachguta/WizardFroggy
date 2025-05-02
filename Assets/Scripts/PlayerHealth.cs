using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;
    private Coroutine burnCoroutine;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"����� ������� {damage} �����. ������� ��������: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void StartBurning(float burnDamage, float duration)
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine); // ���� �������� ��� �����, ���������� ������
        }

        burnCoroutine = StartCoroutine(BurnEffect(burnDamage, duration));
    }

    private IEnumerator BurnEffect(float burnDamage, float duration)
    {
        Debug.Log(" ����� ���������!");
        float elapsed = 0f;

        while (elapsed < duration)
        {
            TakeDamage(burnDamage * Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log(" ������� �����������.");
        burnCoroutine = null;
    }

    private void Die()
    {

        GameObject.Destroy(gameObject);
        gameManager.GameOver();

    }
    public float health { get { return currentHealth; } }
}
