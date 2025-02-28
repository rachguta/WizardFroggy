using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireZoneManager : MonoBehaviour
{
    public Tilemap[] fireStages; // ��� ������� Tilemap (0 - �������, 1 - �����, 2 - ������ �����)
    public Transform gridTransform; // ������ �� ������������ Grid � �����
    private int currentStage = 0;
    private Tilemap activeTilemap;

    public void StartMyCoroutine()
    {
        Debug.Log("�������� ��������");
        StartCoroutine(FireSequence());
    }

    private IEnumerator FireSequence()
    {
        Debug.Log("�������� �����������");
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

            // ���� ��� ������� ������, ���� 1 �������, ����� 2 �������
            yield return new WaitForSeconds(currentStage == 0 || currentStage == 2 || currentStage == 4 ? 1f : 2f);

            // ������� � ���������� �����
            currentStage++;
        }
        if (activeTilemap != null)
        {
            Destroy(activeTilemap.gameObject);
        }
    }
}
