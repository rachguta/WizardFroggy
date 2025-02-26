using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
public class WindEffect : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10000f; // ���� �����
    [SerializeField]
    private float windDuration = 20f; // ������������ �������� ����� � ��������

    [SerializeField] private Rigidbody2D rb;
    private bool isWindActive;

    private readonly Vector2[] windDirections = { Vector2.down, Vector2.left, Vector2.right }; // ������������������ �����������
    private int currentDirectionIndex = 0; // ������� ������ �����������

    private float directionChangeInterval = 5f; // �������� ����� ����������� � ��������

    
    [SerializeField] private bool windCanBlow;
   

    //void Start()
    //{
        

    //    if (rb == null)
    //    {
    //        Debug.LogError("Rigidbody2D �� ���������� ��� WindEffect. ������� ���� � ����������.");
    //        return;
    //    }
    //}

    private IEnumerator ApplyWind()
    {
        isWindActive = true;
        float elapsedTime = 0f;

        while (elapsedTime < windDuration)
        {
            Vector2 currentDirection = windDirections[currentDirectionIndex];

            // ��������� ���� ����� � ������� ����������� ������ ����
            rb.AddForce(currentDirection * windForce * Time.fixedDeltaTime);

            // ��� �������� ����� �����������
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime % directionChangeInterval < Time.fixedDeltaTime)
            {
                // ��������� � ���������� �����������
                currentDirectionIndex = (currentDirectionIndex + 1) % windDirections.Length;
            }

            yield return new WaitForFixedUpdate(); // ��� ��������� ����������
            
        }

        isWindActive = false;
    }
    public void StartMyCoroutine()
    {
        StartCoroutine(ApplyWind());
    }
    
}
