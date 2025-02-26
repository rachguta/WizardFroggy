using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] private bool isEnemyProjectile;
    private float projectileDamage = 7f;
    private bool isReflected = false;
    // Awake is called when the Projectile GameObject is instantiated
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
                playerHealth.TakeDamage(projectileDamage);
            }
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
