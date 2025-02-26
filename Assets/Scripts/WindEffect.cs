using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
public class WindEffect : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10000f; // Сила ветра
    [SerializeField]
    private float windDuration = 20f; // Длительность действия ветра в секундах

    [SerializeField] private Rigidbody2D rb;
    private bool isWindActive;

    private readonly Vector2[] windDirections = { Vector2.down, Vector2.left, Vector2.right }; // Последовательность направлений
    private int currentDirectionIndex = 0; // Текущий индекс направления

    private float directionChangeInterval = 5f; // Интервал смены направления в секундах

    
    [SerializeField] private bool windCanBlow;
   

    //void Start()
    //{
        

    //    if (rb == null)
    //    {
    //        Debug.LogError("Rigidbody2D не установлен для WindEffect. Укажите цель в инспекторе.");
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

            // Применяем силу ветра в текущем направлении каждый кадр
            rb.AddForce(currentDirection * windForce * Time.fixedDeltaTime);

            // Ждём интервал смены направления
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime % directionChangeInterval < Time.fixedDeltaTime)
            {
                // Переходим к следующему направлению
                currentDirectionIndex = (currentDirectionIndex + 1) % windDirections.Length;
            }

            yield return new WaitForFixedUpdate(); // Ждём следующее обновление
            
        }

        isWindActive = false;
    }
    public void StartMyCoroutine()
    {
        StartCoroutine(ApplyWind());
    }
    
}
