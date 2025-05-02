using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    public float parryRadius = 2f; 
    public float parryDuration = 0.25f; 
    public Animator animator;
    private const string PARRY = "Parry";
    private bool isParrying = false;
    public bool canParry;
    public void StartParry(InputAction.CallbackContext context)
    {
        if (!isParrying && canParry)
        {
            StartCoroutine(PerformParry());
        }
    }
    

    private IEnumerator PerformParry()
    {
        
        isParrying = true;
        Debug.Log("Парирование активировано!");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, parryRadius);
        foreach (Collider2D col in colliders)
        {
            Projectile projectile = col.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Reflect(); 
            }
        }
        animator.SetTrigger(PARRY);

        yield return new WaitForSeconds(parryDuration);
        if (animator != null)
        {
            animator.ResetTrigger(PARRY);
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
