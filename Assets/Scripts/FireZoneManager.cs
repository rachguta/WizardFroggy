using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireZoneManager : MonoBehaviour
{
    public Tilemap[] fireStages; 
    public Transform gridTransform; // Ссылка на существующий Grid в сцене
    private int currentStage = 0;
    private Tilemap activeTilemap;
    [SerializeField] private bool onlyChihai;
    private void Start()
    {
        if (onlyChihai) StartCoroutine(FireSequence());
    }
    public void StartMyCoroutine()
    {
        StartCoroutine(FireSequence());
    }

    private IEnumerator FireSequence()
    {
        while (currentStage < fireStages.Length)
        {
            // Удаляем предыдущий Tilemap (если есть)
            if (activeTilemap != null)
            {
                Destroy(activeTilemap.gameObject);
            }

            // Создаем новый Tilemap внутри Grid
            activeTilemap = Instantiate(fireStages[currentStage], gridTransform);
            activeTilemap.transform.localPosition = Vector3.zero; // Обнуляем позицию, чтобы совпадало с Grid

           
            yield return new WaitForSeconds(currentStage % 2 == 0 ? 2f : 3f);

            currentStage++;
        }
        Destroy(activeTilemap.gameObject);
        currentStage = 0;
    }
}
