using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] private bool isEnemyProjectile;
    [SerializeField] private float enemyProjectileDamage = 7f;
    [SerializeField] private float frogProjectileDamage = 1f;
    private bool isReflected = false;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isEnemyProjectile)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyProjectileDamage);
            }
        }
        if (other.CompareTag("Enemy") && !isEnemyProjectile)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(frogProjectileDamage);
            }
            Debug.Log(enemyHealth.health);

        }
        Destroy(gameObject);
        
    }
    public void Reflect()
    {
        if (!isReflected && isEnemyProjectile)
        {
            isReflected = true;
            rigidbody2d.velocity = -rigidbody2d.velocity; // Меняем направление снаряда
            Debug.Log(" Снаряд отражён!");
        }
    }


}
