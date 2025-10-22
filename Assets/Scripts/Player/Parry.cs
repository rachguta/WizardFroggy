using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    public Animator animator;
    
    [SerializeField] private float activeDuration = 0.5f; 
    [SerializeField] private AnimationCurve scaleCurve;   
    [SerializeField] private float reflectionForce = 1000f;
    [SerializeField] private AudioClip parrySound;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Animator healingAnimator;
    [SerializeField] private float healingByParry = 0.85f;
    private PlayerMana mana;
    private Vector3 minScale = Vector3.zero;
    private Vector3 maxScale = Vector3.one;
    private float timer;
    private bool isActive = false;
    
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        mana = GetComponentInParent<PlayerMana>();
    }
    

    public void Activate(Vector3 localScale, float spendingMana)
    {
        if (isActive) return;

        AudioManager.instance.PlaySoundFXClip(parrySound, transform, 0.15f);
        mana.SpendMana(spendingMana);
        animator.SetTrigger("Parry");
        isActive = true;
        timer = 0f;
        maxScale = localScale; 
    }
    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        if (timer >= activeDuration)
        {
            Deactivate();
            return;
        }

        // Плавное изменение размера по кривой
        float t = timer / activeDuration;
        float scale = scaleCurve.Evaluate(t);
        transform.localScale = maxScale * scale;
    }
    private void Deactivate()
    {
        isActive = false;
        transform.localScale = minScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            Vector2 direction = (projectile.transform.position - transform.position).normalized;
            projectile.Reflect(direction, reflectionForce);
            playerHealth.Healing(healingByParry);
            healingAnimator.SetTrigger("Healing");
        }
    }

    
}
