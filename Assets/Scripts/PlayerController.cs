using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashDistance = 2f;
    public float dashCooldown = 0.1f;

    
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isDashing = false;
    public bool canDash;

    [Header("Input")]
    private PlayerInputActions playerInputActions;

    [Header("Health Settings")]
    public int maxHealth = 1000;
    private int currentHealth;
    public float timeInvincible = 0.3f;
    private bool isInvincible = false;
    private float invincibleTimer;

    [Header("Projectile Settings")]
    private float projectileForce = 1000f;
    public GameObject projectilePrefab;
    public bool canShoot;

    [Header("Interaction Settings")]
    [SerializeField] private float interactRadius = 1.5f;
    private Book book;

    //Parry
    private Parry parry;

    //Fire
    private float damageByFire = 3f;
    Vector2 moveDirection = new Vector2(0, 1);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parry = GetComponentInChildren<Parry>();
        currentHealth = maxHealth;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Powers.performed += Powers;
        playerInputActions.Player.Teleport.performed += Teleport;
        playerInputActions.Player.Parry.performed += parry.StartParry;
        playerInputActions.Player.Interaction.performed += FindBook;
    }


    private void Powers(InputAction.CallbackContext context)
    {
        if(canShoot && !isDashing)
        {
            Launch();
        }
    }

    private void Teleport(InputAction.CallbackContext context)
    {
        if(canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FindBook(InputAction.CallbackContext context)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (Collider2D collider in colliders)
        {
            book = collider.GetComponent<Book>();
            if (book != null)
            {
                break;
            }
        }
        if (book != null)
        {
            if (book.isFirstBook)
            {
                canDash = true;
                Debug.Log(" Игрок получил способности: рывок!");

            }
            else if (book.isSecondBook)
            {
                if (parry != null)
                {
                    parry.canParry = true;
                    Debug.Log("Игрок получил способность: парирование!");
                }
                else
                {
                    Debug.LogWarning(" Parry не найден внутри Player!");
                }

            }
            else if (book.isThirdBook)
            {
                canShoot = true;
                Debug.Log(" Игрок получил способность: стрельба!");
                Debug.Log(" Игра началась!");
            }
            Destroy(book.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    void Update()
    {
        movement = playerInputActions.Player.Move.ReadValue<Vector2>();

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

       
    }
    

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = movement.normalized * moveSpeed;

        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        isInvincible = true;
        invincibleTimer = timeInvincible;

        Vector2 dashDirection = movement != Vector2.zero ? movement.normalized : Vector2.up;
        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + dashDirection * dashDistance;

        int layerMask = ~LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(startPosition, dashDirection, dashDistance, layerMask);

        Debug.DrawLine(startPosition, targetPosition, Color.red, 1.0f);

        if (hit.collider != null)
        {
            targetPosition = hit.point - dashDirection * 0.1f; 
            Debug.Log("Obstacle detected: " + hit.collider.name);
        }

        float dashTime = 0.2f; 
        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    
    private void  Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, projectileForce);


    }
}
