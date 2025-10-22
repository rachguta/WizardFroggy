using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ProjectileType
{
    PlayerProjectile,
    EnemyProjectile
}

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public ProjectileType projType;
    [SerializeField] private float damage = 7f;
    [SerializeField] private AudioClip stubbingSound;

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
        if(other.CompareTag("Player") && projType == ProjectileType.EnemyProjectile)
        {
            AudioManager.instance.PlaySoundFXClip(stubbingSound, transform, 0.5f);
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        if (other.CompareTag("Enemy") && projType == ProjectileType.PlayerProjectile)
        {
            
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Debug.Log(enemyHealth.CurrentHealth);

        }
        if(!other.CompareTag("Parry"))
        {
            Destroy(gameObject);
        }
        
    }
    public void Reflect(Vector2 reflectDirection, float force)
    {
        float angle = Mathf.Atan2(reflectDirection.y, reflectDirection.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0,0,angle);
        rigidbody2d.AddForce(reflectDirection * force);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }


}
