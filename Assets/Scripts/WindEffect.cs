using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
public class WindEffect : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10000f; 
    [SerializeField]
    private float windDuration = 20f; 

    [SerializeField] private Rigidbody2D rb;
    private bool isWindActive;

    private readonly Vector2[] windDirections = { Vector2.down, Vector2.left, Vector2.right, Vector2.up }; 
    private int currentDirectionIndex = 0; 

    private float directionChangeInterval = 5f; 
    private bool windCanBlow;
    [SerializeField] private bool onlyChmops;
    private void Start()
    {
        if (onlyChmops) StartCoroutine(ApplyWind());
    }
    private IEnumerator ApplyWind()
    {
        isWindActive = true;
        float elapsedTime = 0f;
        currentDirectionIndex = Random.Range(0, windDirections.Length);
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
                int newIndex = 0;
                do
                {
                    newIndex = Random.Range(0, windDirections.Length);
                }
                while (newIndex == currentDirectionIndex);

                currentDirectionIndex = newIndex;
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
