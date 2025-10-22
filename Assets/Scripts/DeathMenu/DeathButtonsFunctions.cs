using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathButtonsFunctions : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Intro");
    }
}
