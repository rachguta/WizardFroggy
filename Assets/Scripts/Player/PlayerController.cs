using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Vector2 lookDirection = new Vector2(0, 1);  
    private Rigidbody2D rb;


    [Header("Invincibility Settings")]
    [SerializeField] private float speedMultiplier = 2.1f;
    [HideInInspector] public bool isInvincible = false;
    public bool canBeInvincible;

    [Header("Projectile Settings")]
    [SerializeField] private float projectileCooldown = 0.3f;
    private float projectileForce = 1000f;
    public GameObject projectilePrefab;
    public bool canShoot;
    Vector2 projectileDirection = new Vector2(0, 1);
    private float cooldownTimer;


    [Header("Parry")]
    [SerializeField] private Vector3 shieldScale = new Vector3(2f, 2f, 2f); // Размер щита
    public bool canParry;
    [HideInInspector] public Parry parry;

    //Mana
    [Header("Mana")]
    [SerializeField] private PlayerMana mana;
    [SerializeField] private float spendingManaByProjectile = 30f;
    [SerializeField] private float spendingManaByInvincibilityBySecond = 10f;
    [SerializeField] private float spendingManaByParry = 10f;

    [Header("Sound")]
    [SerializeField] private AudioClip ballLightningSound;
    [SerializeField] private AudioClip chargeSound;
    private AudioSource audioSource;


    //Animations
    [HideInInspector] public Animator animator;

    //ABC
    [HideInInspector] public bool hasABC;

    //Health
    private PlayerHealth playerHealth;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parry = GetComponentInChildren<Parry>();
        animator = GetComponent<Animator>();
        cooldownTimer = projectileCooldown;
        audioSource = GetComponent<AudioSource>(); 
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Shoot()
    {
        if(canShoot && !isInvincible && mana.CurrentMana >= spendingManaByProjectile && InputManager.instance.ShootInput)
        {
            Launch();
        }
    }

    private void Parry()
    {
        if (InputManager.instance.ParryInput && !isInvincible && canParry && mana.CurrentMana >= spendingManaByParry)
        {
            parry.Activate(shieldScale, spendingManaByParry);
        }
    }
    private void Move()
    {
        movement = InputManager.instance.PlayerMovement;
        if(!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            lookDirection = movement; 
        }

        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        animator.SetFloat("Speed", movement.magnitude);

    }
    private void Invincibility()
    {
        if(canBeInvincible)
        {
            if (InputManager.instance.InvincibilityInput && mana.CurrentMana >= 1f)
            {
                
                audioSource.clip = ballLightningSound;
                audioSource.volume = 0.3f;
                audioSource.loop = true;
                audioSource.Play();
                gameObject.layer = LayerMask.NameToLayer("Invincibility");
                isInvincible = true;
                mana.isSpending = true;
                animator.SetBool("Is Invincible", true);
            }
            if (InputManager.instance.InvincibilityRelease || InputManager.PlayerInput.currentActionMap.name != "Player" || mana.CurrentMana <= 1f)
            {
                audioSource.clip = null;
                audioSource.loop = false;
                audioSource.volume = 1f;
                audioSource.Stop();
                gameObject.layer = LayerMask.NameToLayer("Player");
                isInvincible = false;
                mana.isSpending = false;
                animator.SetBool("Is Invincible", false);

            }
        }
        
    }
    void Update()
    {
        Move();
        Shoot();
        Invincibility();
        Parry();


        if (cooldownTimer != projectileCooldown)
        {
            cooldownTimer = Mathf.Clamp(cooldownTimer + Time.deltaTime, 0, projectileCooldown);
        }       
    }


    void FixedUpdate()
    {
        if (!isInvincible)
        {
            rb.velocity = movement.normalized * moveSpeed;
        }
        else
        {
            rb.velocity = movement.normalized * moveSpeed * speedMultiplier;
            mana.SpendMana(spendingManaByInvincibilityBySecond * Time.fixedDeltaTime);
        }
    }
    private void  Launch()
    {
        if(cooldownTimer == projectileCooldown)
        {
            AudioManager.instance.PlaySoundFXClip(chargeSound, transform, 0.1f);
            cooldownTimer = 0;
            mana.SpendMana(spendingManaByProjectile);
            GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(projectileDirection, projectileForce);
        }
    }
}
