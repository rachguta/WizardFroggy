using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireZoneManager : MonoBehaviour
{
    public Tilemap[] fireStages; // Три префаба Tilemap (0 - красные, 1 - огонь, 2 - другой огонь)
    public Transform gridTransform; // Ссылка на существующий Grid в сцене
    private int currentStage = 0;
    private Tilemap activeTilemap;

    public void StartMyCoroutine()
    {
        Debug.Log("Действие работает");
        StartCoroutine(FireSequence());
    }

    private IEnumerator FireSequence()
    {
        Debug.Log("Корутина запустилась");
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

            // Если это красные плитки, ждем 1 секунду, иначе 2 секунды
            yield return new WaitForSeconds(currentStage == 0 || currentStage == 2 || currentStage == 4 ? 1f : 2f);

            // Переход к следующему этапу
            currentStage++;
        }
        if (activeTilemap != null)
        {
            Destroy(activeTilemap.gameObject);
        }
    }
}
