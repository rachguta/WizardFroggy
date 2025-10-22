using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private float maxManaValue = 100f;
    [SerializeField] private float fillingSpeed = 15f;
    [HideInInspector] public bool isSpending = false;
    private float currentManaValue;

    public float CurrentMana {  get { return currentManaValue; } }

    public float MaxMana { get { return maxManaValue; } }
    private void Start()
    {
        currentManaValue = maxManaValue;
    }
    public void SpendMana(float value)
    {
        currentManaValue = Mathf.Clamp(currentManaValue - value, 0, maxManaValue);
    }
    private void Update()
    {
        if (currentManaValue != maxManaValue && !isSpending)
        {
            currentManaValue = Mathf.Clamp(currentManaValue + fillingSpeed * Time.deltaTime, 0, maxManaValue);
        }
    }

}
