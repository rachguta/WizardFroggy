using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool startGame;
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

    public UnityEvent OnBossStage1Start;  // Событие начала первой стадии босса
    public UnityEvent OnLearnAbilities1;  // Событие начала обучения
    public UnityEvent OnBossStage2;    // Событие начала исследования мира
    public UnityEvent OnLearnAbilities2;
    public UnityEvent OnBossStage3;



    void Start()
    {
        if(startGame) StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        //Этап 1: Первая стадия босса
        SetGameState(GameState.BossStage1);
        yield return new WaitForSeconds(21); // Ждём 10 секунд (или другую логику завершения)

        // Этап 2: Обучение способностям
        SetGameState(GameState.LearnAbilities1);
        yield return new WaitForSeconds(10); // Ждём 15 секунд

        // Этап 3: Вторая стадия
        SetGameState(GameState.BossStage2);
        yield return new WaitForSeconds(11f);

        SetGameState(GameState.LearnAbilities2);
        yield return new WaitForSeconds(10); 
        // Исследование продолжается
        SetGameState(GameState.BossStage3);
        

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
        }
    }
}
