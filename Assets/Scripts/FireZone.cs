using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField] private float damage = 3f;
    [SerializeField] private Color warningColor = new Color(1f, 0.5f, 0f, 0.5f);
    [SerializeField] private Color fireColor = new Color(1f, 0f, 0f, 1f);

    private SpriteRenderer spriteRenderer;
    private Collider2D fireCollider;
    [SerializeField]private bool isActive = false;
    private float burnDuration = 2f;

    void Start()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        fireCollider = GetComponent<Collider2D>();

        //if (spriteRenderer == null) Debug.LogError(" SpriteRenderer не найден!");
        //if (fireCollider == null) Debug.LogError(" Collider2D не найден!");

        //fireCollider.enabled = false;
    }

    

    private void OnTriggerStay2D(Collider2D other)
    {

        if (isActive && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.StartBurning(damage, burnDuration);
            }
        }
    }
}
