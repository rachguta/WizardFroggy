using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireZoneManager : MonoBehaviour
{
    public Tilemap[] fireStages; 
    public Transform gridTransform; // ������ �� ������������ Grid � �����
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
            // ������� ���������� Tilemap (���� ����)
            if (activeTilemap != null)
            {
                Destroy(activeTilemap.gameObject);
            }

            // ������� ����� Tilemap ������ Grid
            activeTilemap = Instantiate(fireStages[currentStage], gridTransform);
            activeTilemap.transform.localPosition = Vector3.zero; // �������� �������, ����� ��������� � Grid

           
            yield return new WaitForSeconds(currentStage % 2 == 0 ? 2f : 3f);

            currentStage++;
        }
        Destroy(activeTilemap.gameObject);
        currentStage = 0;
    }
}
