using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public float parryRadius = 2f; // ������ ����������� (180 �������� �����)
    public float parryDuration = 0.2f; // ������������ �����������
    public Animator animator;
    private const string PARRY = "Parry";
    private bool isParrying = false;
    public bool canParry;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !isParrying && canParry)
        {
            StartCoroutine(PerformParry());
        }
    }

    private IEnumerator PerformParry()
    {
        if (animator != null)
        {
            animator.SetTrigger(PARRY);
        }
        isParrying = true;
        Debug.Log("����������� ������������!");

        // ��������� ��� ������� � ������� 2D-�����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, parryRadius);
        foreach (Collider2D col in colliders)
        {
            Projectile projectile = col.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Reflect(); // �������� ������
            }
        }

        yield return new WaitForSeconds(parryDuration);
        if (animator != null)
        {
            animator.ResetTrigger("parry");
        }
        isParrying = false;
        Debug.Log(" ����������� ���������.");
    }

    // ������������ ���� �����������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, parryRadius);
    }
}
