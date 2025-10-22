using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator mopsAnimator;
    [SerializeField] private Animator chihAnimator;
    [SerializeField] private Animator bulAnimator;
    [SerializeField] private AudioClip damageSound;

    [SerializeField] private GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public float CurrentHealth 
    {
        get
        {
            if (currentHealth < 0)
            {
                return 0;
            }
            else
            {
                return currentHealth;
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        AudioManager.instance.PlaySoundFXClip(damageSound, transform, 0.6f);
        HealthStagesAnimation(currentHealth);
        StartCoroutine(PainAnimation());
        Debug.Log($"Цербер получил {damage} урона. Текущее здоровье: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator PainAnimation()
    {
        mopsAnimator.SetBool("Damage", true);
        chihAnimator.SetBool("Damage", true);
        bulAnimator.SetBool("Damage", true);
        yield return new WaitForSeconds(0.3f);
        mopsAnimator.SetBool("Damage", false);
        chihAnimator.SetBool("Damage", false);
        bulAnimator.SetBool("Damage", false);


    }
    private void HealthStagesAnimation(float currentHealth)
    {
        if(currentHealth >= 40f && currentHealth < 70f && !mopsAnimator.GetBool("70hp"))
        {
            mopsAnimator.SetBool("70hp", true);
        }
        else if(currentHealth >= 15f && currentHealth < 40f && !bulAnimator.GetBool("40hp"))
        {
            bulAnimator.SetBool("40hp", true);
        }
        else if(currentHealth < 15f && !chihAnimator.GetBool("15hp"))
        {
            chihAnimator.SetBool("15hp", true);
        }
        else
        {
            return;
        }
    }
    private void Die()
    {
        if (!gameManager.isGoodEndingActive && !gameManager.isBadEndingActive)
        {
            gameManager.GameOver();
            gameManager.StartGoodEnding();
        }

    }


}
