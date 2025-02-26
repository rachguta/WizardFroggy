using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform shootPoint; // Точка выстрела
    public float shootInterval = 1f; // Интервал между выстрелами
    public float projectileForce = 5f; // Сила выстрела
    public float shooterDuration = 10f; // Время работы

    private bool isShooting = true;
    [SerializeField]private bool onlyBulBul;
    private void Start()
    {
        if (onlyBulBul)
        {
            StartMyCoroutines();
        }
    }
    public void StartMyCoroutines()
    {
        StartCoroutine(ShootRoutine());
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

    private void Shoot()
    {
        // Генерируем случайный угол от -90° до 90°
        float randomAngle = Random.Range(-85f, 0f);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.down;

        // Создаём снаряд
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Получаем компонент `Projectile` и вызываем Launch()
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Launch(direction, projectileForce);
        }
    }

    private IEnumerator StopShootingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShooting = false; // Останавливаем цикл стрельбы
        Debug.Log(" RandomShooter остановился!");
    }
}
