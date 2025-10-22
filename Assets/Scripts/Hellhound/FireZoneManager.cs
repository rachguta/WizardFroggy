using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireZoneManager : MonoBehaviour
{
    public FireFloor[] fireFloors; 
    [SerializeField] private Transform gridTransform;
    [SerializeField] private int countOfFireFloors = 3;
    [SerializeField] private bool onlyChihai;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float attentionDuration;
    [SerializeField] private float fireFloorDuration;
    [HideInInspector] public Tilemap activeTilemap;
    [HideInInspector] public bool isFireStageActive;
    public GameObject fireAboveHead;
    [SerializeField] private GameManager gameManager;
    private int currentStage;

   
    private void Start()
    {
        if (onlyChihai) StartCoroutine(FireSequence());
    }
    public void StartTheFire()
    {
        StartCoroutine(FireSequence());
        fireAboveHead.SetActive(true);
    }

    private IEnumerator FireSequence()
    {
        isFireStageActive = true;
        currentStage = Random.Range(0, fireFloors.Length);
        for (int i = 0; i < countOfFireFloors; i++)
        {
            //Удаляем предыдущий Tilemap(если есть)
            if (activeTilemap != null)
            {
                Destroy(activeTilemap.gameObject);
            }
            //ATTENTION
            activeTilemap = Instantiate(fireFloors[currentStage].attentionMap, gridTransform);
            activeTilemap.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(attentionDuration);

            //Fire
            Destroy(activeTilemap.gameObject);
            AudioManager.instance.PlaySoundFXClip(fireSound, transform, 0.3f, 2f);
            activeTilemap = Instantiate(fireFloors[currentStage].fireMap, gridTransform);
            activeTilemap.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(fireFloorDuration);

            //Генерируем новый FireFloor
            int nextStage;
            do
            {
                nextStage = Random.Range(0, fireFloors.Length);
            }
            while (nextStage == currentStage);
            currentStage = nextStage;
        }
        Destroy(activeTilemap.gameObject);
        fireAboveHead.SetActive(false);
        //additionalEffectsAnimator.SetBool("Chih Fire", false);
        isFireStageActive = false;
    }
}
