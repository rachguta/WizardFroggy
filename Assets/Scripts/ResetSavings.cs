using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSavings : MonoBehaviour
{
    public void ResetPlayerProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Все сохранения удалены!");
    }

}
