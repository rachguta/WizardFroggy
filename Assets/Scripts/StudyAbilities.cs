using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyAbilities : MonoBehaviour
{
    public GameObject book1Prefab;
    [SerializeField] private bool isRightBookcase;
    [SerializeField] private bool isLeftBookcase;
    public void InitiateBook1()
    {

        if (isLeftBookcase)
        {

            GameObject book1 = Instantiate(book1Prefab, (Vector2)transform.position + Vector2.right * 3, Quaternion.identity);
        }
        else if(isRightBookcase)
        {
            GameObject book1 = Instantiate(book1Prefab, (Vector2)transform.position + Vector2.left * 3, Quaternion.identity);
        }
    }
}
