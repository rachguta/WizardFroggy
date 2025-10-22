using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
public enum FireColor
{
    Red,
    Green
}

public class FireZone : MonoBehaviour
{
    [SerializeField] private FireColor fireColor;
    [SerializeField] private float damage = 3f;
    private Collider2D fireCollider;
    private float burnDuration = 2f;

    void Start()
    {
        fireCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (fireColor == FireColor.Red)
                {
                    playerHealth.StartBurning(damage, burnDuration, "Red");
                }
                else if (fireColor == FireColor.Green)
                {
                    playerHealth.StartBurning(damage, burnDuration, "Green");
                }
            }
        }
    }
}
