using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomShooter : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private Vector3[] portalPositions;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private float shootInterval = 1f; 
    [SerializeField] private float projectileFlyForce = 5f; 
    [SerializeField] private float shooterDuration = 15f; 
    [SerializeField] private AudioClip knifeFlySound;
    [SerializeField] private GameObject projectilePrefab; 
    public GameObject fireAboveHead;
    [SerializeField] private bool onlyBulBul;
    public SpriteRenderer portalSpriteRenderer;
    private int positionIndex;
    private Transform shootPoint; 
    [HideInInspector] public bool isShooting = false;

    private void Start()
    {
        shootPoint = portal.transform;
        if (onlyBulBul)
        {
            StartShooting();
        }

    }
    public void StartShooting()
    {
        AudioManager.instance.PlayLoopSound(portalSound, transform, 0.3f, shooterDuration);
        portalSpriteRenderer.enabled = true;
        positionIndex = Random.Range(0, portalPositions.Length);
        portal.transform.position = portalPositions[positionIndex];
        isShooting = true;
        fireAboveHead.SetActive(true);
        StartCoroutine(ShootRoutine());
        StartCoroutine(ChangePortalPosition());
        StartCoroutine(StopShootingAfterTime(shooterDuration));
    }

    private IEnumerator ShootRoutine()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }   
    }

    private IEnumerator ChangePortalPosition()
    {
        while(isShooting)
        {
            portalSpriteRenderer.flipX = positionIndex == 1 ? true : false;
            yield return new WaitForSeconds(shooterDuration / 2);
            int newPositionIndex;
            do
            {
                newPositionIndex = Random.Range(0, portalPositions.Length);
            }
            while (newPositionIndex == positionIndex);
            positionIndex = newPositionIndex;
            portal.transform.position = portalPositions[positionIndex];
            Debug.Log(portal.transform.position);
        }
    }

    private void Shoot()
    {
        float randomAngle;
        // Генерируем случайный угол 
        if (portal.transform.position.x > 0) //Если портал справа
        {
            randomAngle = Random.Range(-93f, 0f);
        }
        else //Если портал слева
        {
            randomAngle = Random.Range(93f, 0f);
        }
        Quaternion rotation = Quaternion.Euler(0,0,randomAngle);
        Vector2 direction = rotation * Vector2.down;

        // Создаём снаряд
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, rotation);
        AudioManager.instance.PlaySoundFXClip(knifeFlySound, transform, 0.1f);
        // Получаем компонент Projectile и вызываем Launch()
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Launch(direction, projectileFlyForce);
        }
    }

    private IEnumerator StopShootingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        portalSpriteRenderer.enabled = false;
        portalSpriteRenderer.flipX = false;
        isShooting = false; // Останавливаем цикл стрельбы
        Debug.Log("RandomShooter остановился!");
        yield return new WaitForSeconds(1);
        fireAboveHead.SetActive(false);
    }
    
}
