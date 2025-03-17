using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Book : MonoBehaviour
{
    public float interactRadius = 1.5f; 
    private PlayerController player; 
    [SerializeField] bool isFirstBook;
    [SerializeField] bool isSecondBook;
    [SerializeField] bool isThirdBook;
    private void Update()
    {
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
                Debug.Log(" ����� ������� �����������: �����!");
                
            }
            else if (isSecondBook)
            {
                Parry parry = player.GetComponentInChildren<Parry>();
                if (parry != null)
                {
                    parry.canParry = true;
                    Debug.Log("����� ������� �����������: �����������!");
                }
                else
                {
                    Debug.LogWarning(" Parry �� ������ ������ Player!");
                }

            }
            else if (isThirdBook)
            {
                player.canShoot = true;
                Debug.Log(" ����� ������� �����������: ��������!");
                Debug.Log(" ���� ��������!");

            }

            Destroy(gameObject); 
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
