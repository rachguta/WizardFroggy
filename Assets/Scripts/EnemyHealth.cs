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
        Debug.Log($"������ ������� {damage} �����. ������� ��������: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log(" ������ �����!");
        GameObject.Destroy(gameObject);

        // ����� ����� �������� �������� ������
    }
    public float health { get { return currentHealth; } }


}
