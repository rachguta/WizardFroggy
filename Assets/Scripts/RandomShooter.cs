using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // ������ �������
    [SerializeField] private Transform shootPoint; // ����� ��������
    [SerializeField] private float shootInterval = 1f; // �������� ����� ����������
    [SerializeField] private float projectileForce = 5f; // ���� ��������
    [SerializeField] private float shooterDuration = 15f; // ����� ������

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
        // ���������� ��������� ���� �� -90� �� 90�
        float randomAngle = Random.Range(-85f, 0f);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.down;

        // ������ ������
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // �������� ��������� `Projectile` � �������� Launch()
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Launch(direction, projectileForce);
        }
    }

    private IEnumerator StopShootingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShooting = false; // ������������� ���� ��������
        Debug.Log(" RandomShooter �����������!");
        yield return new WaitForSeconds(3);
        isShooting = true;
    }
}
