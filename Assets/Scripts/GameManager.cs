using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool startGame;
    private bool isPaused;
    private PlayerInputActions playerInputActions;
    private bool isCooldownActive = false;
    private float resumeTime = 0f; 
    void Start()
    {
        if (startGame) StartCoroutine(GameFlow());
        playerInputActions = new PlayerInputActions();
        playerInputActions.GameManagment.Enable();
        playerInputActions.GameManagment.Pause.performed += TogglePause;
    }

    private void Update()
    {
        if (isCooldownActive && Time.unscaledTime >= resumeTime)
        {
            ResumeGame();
        }
    }
    public enum GameState
    {
        BossStage1,
        LearnAbilities1,
        BossStage2,
        LearnAbilities2,
        BossStage3,
        LearnAbilities3
    }

    [SerializeField]
    private GameState currentState = GameState.BossStage1;

    public UnityEvent OnBossStage1Start;  
    public UnityEvent OnLearnAbilities1;  
    public UnityEvent OnBossStage2;    
    public UnityEvent OnLearnAbilities2;
    public UnityEvent OnBossStage3;
    public UnityEvent OnLearnAbilities3;


    

    private IEnumerator GameFlow()
    {
        SetGameState(GameState.BossStage1);
        yield return new WaitForSeconds(15);

        SetGameState(GameState.LearnAbilities1);
        yield return new WaitForSeconds(7); 

        SetGameState(GameState.BossStage2);
        yield return new WaitForSeconds(15);

        SetGameState(GameState.LearnAbilities2);
        yield return new WaitForSeconds(7); 

        SetGameState(GameState.BossStage3);
        yield return new WaitForSeconds(15);

        SetGameState(GameState.LearnAbilities3);
        yield return new WaitForSeconds(7);
        Debug.Log("Основной этап игры");

        // Основной игровой процесс: рандомное комбинирование 2 стадий
        StartCoroutine(BossBattleLoop());

    }
    private IEnumerator BossBattleLoop()
    {
        GameState[] bossStages = { GameState.BossStage1, GameState.BossStage2, GameState.BossStage3 };

        while (true) 
        {
            GameState firstStage = bossStages[Random.Range(0, bossStages.Length)];
            GameState secondStage;
            do
            {
                secondStage = bossStages[Random.Range(0, bossStages.Length)];
            }
            while (secondStage == firstStage); 
            SetGameState(firstStage);
            SetGameState(secondStage);
            yield return new WaitForSeconds(19);
        }
    }


    private void SetGameState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Смена состояния: {newState}");

        switch (newState)
        {
            case GameState.BossStage1:
                OnBossStage1Start?.Invoke();
                break;

            case GameState.LearnAbilities1:
                OnLearnAbilities1?.Invoke();
                break;

            case GameState.BossStage2:
                OnBossStage2?.Invoke();
                break;
            case GameState.BossStage3:
                OnBossStage3?.Invoke();
                break;
            case GameState.LearnAbilities2:
                OnLearnAbilities2?.Invoke();
                break;
            case GameState.LearnAbilities3:
                OnLearnAbilities3?.Invoke();
                break;
        }
    }
    public void TogglePause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            isCooldownActive = true;
            resumeTime = Time.unscaledTime + 1f; 
            Debug.Log("Ожидание перед продолжением...");
        }
        else
        {
            Time.timeScale = 0f; 
            isPaused = true;
            Debug.Log("Игра на паузе!");
        }
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        isCooldownActive = false;
        Debug.Log("Игра продолжается!");
    }
    public void GameOver()
    {
        Debug.Log("Game Over!!!");
        Time.timeScale = 0f;
    }
}
