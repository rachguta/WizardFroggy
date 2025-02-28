using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public float parryRadius = 2f; // Радиус парирования (180 градусов вверх)
    public float parryDuration = 0.2f; // Длительность парирования
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
        Debug.Log("Парирование активировано!");

        // Проверяем все объекты в радиусе 2D-круга
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, parryRadius);
        foreach (Collider2D col in colliders)
        {
            Projectile projectile = col.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Reflect(); // Отбиваем снаряд
            }
        }

        yield return new WaitForSeconds(parryDuration);
        if (animator != null)
        {
            animator.ResetTrigger("parry");
        }
        isParrying = false;
        Debug.Log(" Парирование завершено.");
    }

    // Визуализация зоны парирования
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, parryRadius);
    }
}
