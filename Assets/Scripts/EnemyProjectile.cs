using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f; // ���� �������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���� �� � ������� ��������� PlayerHealth
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage); // ������� ���� ������
        }

        // ���������� ������ ��� ����� ������������
        Destroy(gameObject);
    }
}
