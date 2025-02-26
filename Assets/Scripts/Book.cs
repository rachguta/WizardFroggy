using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Book : MonoBehaviour
{
    public float interactRadius = 2f; // ������ ��������������
    private PlayerController player; // ������ �� ������
    [SerializeField] bool isFirstBook;
    [SerializeField] bool isSecondBook;
    private void Update()
    {
        // ���������, ���� �� ����� � �������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                break; // ����� ������, �������
            }
        }

        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (isFirstBook)
            {
                player.canShoot = true;
                Debug.Log(" ����� ������� �����������: ��������!");
            }
            else if (isSecondBook)
            {
                player.canDash = true;
                Debug.Log(" ����� ������� �����������: �����!");
            }
            
            Destroy(gameObject); // ������� ����� ����� �������������
        }
    }

    // ���������� ������ � ���������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
