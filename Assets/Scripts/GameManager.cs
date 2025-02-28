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
    public UnityEvent OnLearnAbilities3;


    void Start()
    {
        if(startGame) StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        //Этап 1: Первая стадия босса
        SetGameState(GameState.BossStage1);
        yield return new WaitForSeconds(21);

        // Этап 2: Обучение способностям
        SetGameState(GameState.LearnAbilities1);
        yield return new WaitForSeconds(10); 

        // Этап 3: Вторая стадия
        SetGameState(GameState.BossStage2);
        yield return new WaitForSeconds(11f);

        SetGameState(GameState.LearnAbilities2);
        yield return new WaitForSeconds(10); 
        SetGameState(GameState.BossStage3);
        yield return new WaitForSeconds(11);
        // Исследование продолжается
        SetGameState(GameState.LearnAbilities3);
        yield return new WaitForSeconds(5);
        Debug.Log("Основной этап игры");

        SetGameState(GameState.BossStage3);
        // Основной игровой процесс: рандомное комбинирование 2 стадий
        //StartCoroutine(BossBattleLoop());

    }
    private IEnumerator BossBattleLoop()
    {
        GameState[] bossStages = { GameState.BossStage1, GameState.BossStage2, GameState.BossStage3 };

        while (true) //  Зацикленный игровой процесс
        {
            // Выбираем две случайные стадии
            GameState firstStage = bossStages[Random.Range(0, bossStages.Length)];
            GameState secondStage;
            do
            {
                secondStage = bossStages[Random.Range(0, bossStages.Length)];
            }
            while (secondStage == firstStage); // Убеждаемся, что выбраны две разные стадии
            SetGameState(firstStage);
            SetGameState(secondStage);
            yield return new WaitForSeconds(15);
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
}
