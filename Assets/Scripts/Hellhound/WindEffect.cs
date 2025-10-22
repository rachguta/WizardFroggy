using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class WindEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float windForce = 10000f; 
    [SerializeField] private float windDuration = 20f; 
    [SerializeField] private float directionChangeInterval = 5f;
    [SerializeField] private float rechargeTime = 1f;
    [SerializeField] private bool onlyChmops;
    public GameObject wind;
    [SerializeField] private AudioClip windSound;
    public GameObject fireAboveHead;
    [HideInInspector] public bool isWindActive = false;
    private readonly Vector2[] windDirections = { Vector2.down, Vector2.left, Vector2.right, Vector2.up }; 
    private int currentDirectionIndex;

    
    private void Start()
    {
        currentDirectionIndex = Random.Range(0, windDirections.Length);
        if (onlyChmops)
        {
            ApplyWind();
        }
    }
    

    public void ApplyWind()
    {
        isWindActive = true;
        fireAboveHead.SetActive(true);
        
        StartCoroutine(WindRoutine());
        StartCoroutine(StopTheWindAfterTime());

    }
    private IEnumerator WindRoutine()
    {
        while (isWindActive)
        {
            StartCoroutine(Blow());
            yield return new WaitForSeconds(directionChangeInterval + rechargeTime);
        }
    }
    private IEnumerator Blow()
    {
        //Генерируем направление
        int newIndex = 0;
        do
        {
            newIndex = Random.Range(0, windDirections.Length);
        }
        while (newIndex == currentDirectionIndex);
        currentDirectionIndex = newIndex;
        SetAnimationTrigger(currentDirectionIndex);
        AudioManager.instance.PlaySoundFXClip(windSound, transform, 0.1f, directionChangeInterval);
        //Толкаем персонажа
        Vector2 currentDirection = windDirections[currentDirectionIndex];
        float elapsedTime = 0f;
        while(elapsedTime < directionChangeInterval)
        {
            rb.AddForce(currentDirection * windForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAnimationTrigger(currentDirectionIndex);
    }
    private IEnumerator StopTheWindAfterTime()
    {
        yield return new WaitForSeconds(windDuration);
        isWindActive = false;
        Debug.Log("Mops остановился!");
        fireAboveHead.SetActive(false);
        //additionalEffectsAnimator.SetBool("Mops Wind", false);
    }
    private void SetAnimationTrigger(int currentIndex)
    {
        switch (currentIndex)
        {
            case 0:
                wind.GetComponent<Animator>().SetTrigger("Down");
                break;
            case 1:
                wind.GetComponent<Animator>().SetTrigger("Left");
                break;
            case 2:
                wind.GetComponent<Animator>().SetTrigger("Right");
                break;
            case 3:
                wind.GetComponent<Animator>().SetTrigger("Up");
                break;
        }

    }


    
    
}
