using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

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

    [Header("Health Settings")]
    public int maxHealth = 1000;
    private int currentHealth;
    public float timeInvincible = 0.3f;
    private bool isInvincible = false;
    private float invincibleTimer;

    //Projectile
    private float projectileForce = 1000f;
    public GameObject projectilePrefab;
    public bool canShoot;

    //Fire
    private float damageByFire = 3f;
    Vector2 moveDirection = new Vector2(0, 1);
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && canShoot && !isDashing)
        {
            Launch();
        }

        //if(Input.GetKeyDown(KeyCode.X))
        //{
        //    FindBook();
        //}

    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            // Apply movement
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

    //ÍÅ ÀÊÒÓÀËÜÍÎ
    //public void ChangeHealth(int amount)
    //{
    //    if (amount < 0)
    //    {
    //        if (isInvincible)
    //            return;

    //        isInvincible = true;
    //        invincibleTimer = timeInvincible;
    //    }
    //    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}
    //IEnumerator FastChangeHealth(int amount)
    //{
    //    float elapsedTime = 0;
    //    while (elapsedTime < damageByFire) 
    //    {
    //        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    //        elapsedTime += Time.fixedDeltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }
        

    //}
    //private void Die()
    //{
    //    Debug.Log("Player has died.");
    //}
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, projectileForce);


    }
    //void FindBook()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(rb.position, movement, 1.5f, LayerMask.GetMask("Book"));
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
    //        canDash = true;
    //    }
    //}
}
