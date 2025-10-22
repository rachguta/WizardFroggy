using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private AudioSource burningAudioSource;
    [SerializeField] private Animator burningAnimator;
    private string currentCondition = null;
    private PlayerController playerController;
    private float currentHealth;
    private Coroutine burnCoroutine;
    public float CurrentHealth
    {
        get
        {
            if(currentHealth < 0)
            {
                return 0;
            }
            else
            {
                return currentHealth;
            }
        }
    }

    public float MaxHealth { get { return maxHealth; } }
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentCondition != null && playerController.canBeInvincible)
        {
            if(InputManager.instance.InvincibilityInput)
            {
                burningAnimator.SetBool(currentCondition, false);
            }
            else if(InputManager.instance.InvincibilityRelease || InputManager.PlayerInput.currentActionMap.name != "Player")
            {
                burningAnimator.SetBool(currentCondition, true);
            }
        }
        
    }

    public void Healing(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, 100);
        Debug.Log($"Игрок получил {health} урона. Текущее здоровье: {currentHealth}");
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

    public void StartBurning(float burnDamage, float duration, string color)
    {
        
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine); // Если персонаж уже горит, сбрасываем таймер
        }
        else if(burnCoroutine == null)
        {
            burningAudioSource.Play();  
        }

        burnCoroutine = StartCoroutine(BurnEffect(burnDamage, duration, color));
    }

    private IEnumerator BurnEffect(float burnDamage, float duration, string color)
    {
        
        if (color == "Red") //Если огонь красный
        {
            if(currentCondition == "Green")
            {
                burningAnimator.SetBool("Green", false);
            }
            if(!burningAnimator.GetBool("Red"))
            {
                burningAnimator.SetBool("Red", true);
                currentCondition = "Red";
            }
        }
        else if (color == "Green") //Если огонь зеленый
        {
            if (currentCondition == "Red")
            {
                burningAnimator.SetBool("Red", false);
            }
            if (!burningAnimator.GetBool("Green"))
            {
                burningAnimator.SetBool("Green", true);
                currentCondition = "Green";
            }
        }
        float elapsed = 0f;

        while (elapsed < duration)
        {
            TakeDamage(burnDamage * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        burningAnimator.SetBool(currentCondition, false);
        burnCoroutine = null;
        currentCondition = null;
        burningAudioSource.Stop();
    }

    private void Die()
    {
        if(!gameManager.isGoodEndingActive && !gameManager.isBadEndingActive)
        {
            gameManager.GameOver();
            gameManager.StartBadEnding();
        }
        
    }
}
