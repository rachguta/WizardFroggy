using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageByBody = 10f;
    [SerializeField] private AudioClip clashSound;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && playerHealth.gameObject.layer != LayerMask.NameToLayer("Invincibility"))
        {
            AudioManager.instance.PlaySoundFXClip(clashSound, transform, 0.5f);
            playerHealth.TakeDamage(damageByBody);
            Debug.Log(playerHealth.CurrentHealth);
        }
    }
    
}
