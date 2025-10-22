using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startGame;
    [Header("Hellhound")]
    [SerializeField] private GameObject mops;
    [SerializeField] private GameObject chih;
    [SerializeField] private GameObject bul;
    [SerializeField] private EnemyHealth hellhoundHealth;
    [SerializeField] private GameObject bodyAndHeads;
    [SerializeField] private Animator hellhoundEffectsAnimator;
    private GameState currentLearningState;
    private WindEffect mopsStage;
    private FireZoneManager chihStage;
    private RandomShooter bulStage;


    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Parry parry;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator playerEffectsAnimator;

    [Header("Dialogues")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogueElements;

    [Header("Cutscene")]
    [SerializeField] private TimelineManager timelineManager;

    [Header("HUD")]
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject manaBar;

    [Header("Green Fire")]
    [SerializeField] private GameObject greenFire;

    [Header("LowerLimit")]
    [SerializeField] private GameObject lowerLimit;

    [Header("DeathMenu")]
    [SerializeField] private GameObject deathMenu;

    [Header("Vingette")]
    [SerializeField] private GameObject vignette;

    [Header("Leaflet")]
    [SerializeField] private GameObject leaflet;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip vingetteSound;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip finalBlowSound;
    private AudioSource mainThemeSource;

    //Endings
    [HideInInspector] public bool isGoodEndingActive;
    [HideInInspector] public bool isBadEndingActive;

    public bool hasCompletedTutorial;


    public enum GameState
    {
        Intro,
        Dialogue1,
        BossStage1,
        LearnAbilities1,
        Dialogue2,
        Dialogue3,
        BossStage2,
        LearnAbilities2,
        Dialogue4,
        BossStage3,
        LearnAbilities3,
        Dialogue5,
        Dialogue6,
        ABC,
        GoodEndingDialogue1,
        GoodEndingDialogue2,
        GoodEndingDialogue3,
        BadEndingDialogue,

    }

    public UnityEvent onIntro;
    public UnityEvent onDialogue1;
    public UnityEvent onBossStage1;
    public UnityEvent onLearnAbilities1;
    public UnityEvent onDialogue2;
    public UnityEvent onDialogue3;
    public UnityEvent onBossStage2;
    public UnityEvent onLearnAbilities2;
    public UnityEvent onDialogue4;
    public UnityEvent onBossStage3;
    public UnityEvent onLearnAbilities3;
    public UnityEvent onDialogue5;
    public UnityEvent onDialogue6;
    public UnityEvent onABC;
    public UnityEvent onGoodEndingDialogue1;
    public UnityEvent onGoodEndingDialogue2;
    public UnityEvent onGoodEndingDialogue3;
    public UnityEvent onBadEndingDialogue;

    void Start()
    {
        mopsStage = mops.GetComponent<WindEffect>();
        chihStage = chih.GetComponent<FireZoneManager>();
        bulStage = bul.GetComponent<RandomShooter>();
        hasCompletedTutorial = PlayerPrefs.GetInt("HasCompletedTutorial", 0) == 1;
        mainThemeSource = GetComponent<AudioSource>();
        if(hasCompletedTutorial)
        {
            greenFire.SetActive(true);
            lowerLimit.SetActive(true);
            playerController.canBeInvincible = true;
            playerController.canParry = true;
            playerController.canShoot = true;
            healthBar.SetActive(true);
            manaBar.SetActive(true);
            mainThemeSource.Play();
            
        }
        if (startGame)
        {
            StartCoroutine(MainGameFlow());
        }
    }

    private IEnumerator MainGameFlow()
    {
        if(hasCompletedTutorial)
        {
            StartCoroutine(BossFight());
        }
        else
        {
            yield return Tutorial();
            StartCoroutine(BossFight());
        }
    }

    private IEnumerator Tutorial()
    {
        //Intro
        InputManager.PlayerInput.SwitchCurrentActionMap("Cutscene");
        SetGameState(GameState.Intro);
        yield return new WaitUntil(() => timelineManager.cutsceneHasEnded);
        //Dialogue 1
        SetGameState(GameState.Dialogue1);
        yield return new WaitUntil(() => dialogueManager.dialogues[0].isPassed);
        mainThemeSource.Play();
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        //Boss stage 1
        healthBar.gameObject.SetActive(true);
        SetGameState(GameState.BossStage1);
        yield return new WaitUntil(() => !mopsStage.isWindActive);
        //Learn abilities 1
        SetGameState(GameState.LearnAbilities1);
        yield return new WaitForSeconds(2);
        //Dialogue 2
        SetGameState(GameState.Dialogue2);
        yield return new WaitUntil(() => dialogueManager.dialogues[1].isPassed);
        yield return new WaitUntil(() => playerController.isInvincible);
        yield return new WaitForSeconds(1);
        //Dialogue 3
        SetGameState(GameState.Dialogue3);
        yield return new WaitUntil(() => dialogueManager.dialogues[2].isPassed);
        yield return new WaitForSeconds(3);
        //Boss stage 2
        SetGameState(GameState.BossStage2);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !chihStage.isFireStageActive);

        //Learn Abilities 2
        SetGameState(GameState.LearnAbilities2);
        yield return new WaitUntil(() => playerController.canParry);
        //Dialogue 4
        SetGameState(GameState.Dialogue4);
        yield return new WaitUntil(() => dialogueManager.dialogues[3].isPassed);
        yield return new WaitForSeconds(3);
        //Boss stage 3
        SetGameState(GameState.BossStage3);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !bulStage.isShooting);
        //Dialogue 5
        yield return new WaitForSeconds(3);
        SetGameState(GameState.Dialogue5);
        yield return new WaitUntil(() => dialogueManager.dialogues[4].isPassed);
        //Learn Abilities 3
        SetGameState(GameState.LearnAbilities3);
        yield return new WaitUntil(() => hellhoundHealth.CurrentHealth <= 99);
        // Сохраняем, что обучение пройдено
        PlayerPrefs.SetInt("HasCompletedTutorial", 1);
        PlayerPrefs.Save(); 
    }

    private IEnumerator BossFight()
    {
        //Dialogue 6
        SetGameState(GameState.Dialogue6);
        yield return new WaitUntil(() => dialogueManager.dialogues[5].isPassed);
        Debug.Log("Основной этап игры");
        yield return new WaitForSeconds(3);
        // Основной игровой процесс: рандомное комбинирование 2 стадий
        StartCoroutine(BossBattleLoop());
    }



    private void SetGameState(GameState newState)
    {
        currentLearningState = newState;
        Debug.Log($"Смена состояния: {newState}");

        switch (newState)
        {
            case GameState.Intro:
                onIntro?.Invoke();
                break;
            case GameState.Dialogue1:
                onDialogue1?.Invoke();
                break;
            case GameState.BossStage1:
                onBossStage1?.Invoke();
                break;
            case GameState.LearnAbilities1:
                onLearnAbilities1?.Invoke();
                break;
            case GameState.Dialogue2:
                onDialogue2?.Invoke();
                break;
            case GameState.Dialogue3:
                onDialogue3?.Invoke();
                break;
            case GameState.BossStage2:
                onBossStage2?.Invoke();
                break;
            case GameState.LearnAbilities2:
                onLearnAbilities2?.Invoke();
                break;
            case GameState.Dialogue4:
                onDialogue4?.Invoke();
                break;
            case GameState.BossStage3:
                onBossStage3?.Invoke();
                break;
            case GameState.LearnAbilities3:
                onLearnAbilities3?.Invoke();
                break;
            case GameState.Dialogue5:
                onDialogue5?.Invoke();
                break;
            case GameState.Dialogue6:
                onDialogue6?.Invoke();
                break;
            case GameState.ABC:
                onABC?.Invoke();
                break;
            case GameState.GoodEndingDialogue1:
                onGoodEndingDialogue1?.Invoke();
                break;
            case GameState.GoodEndingDialogue2:
                onGoodEndingDialogue2?.Invoke();
                break;
            case GameState.GoodEndingDialogue3:
                onGoodEndingDialogue3?.Invoke();
                break;
            case GameState.BadEndingDialogue:
                onBadEndingDialogue?.Invoke();
                break;
        }
    }



    private IEnumerator BossBattleLoop()
    {
        GameState[] bossStages = { GameState.BossStage1, GameState.BossStage2, GameState.BossStage3 };
        GameState previousFirstStage = 0;
        GameState previousSecondStage = 0;
        while (hellhoundHealth.CurrentHealth > 0) 
        {
            GameState firstStage = bossStages[Random.Range(0, bossStages.Length)];
            GameState secondStage;
            do
            {
                secondStage = bossStages[Random.Range(0, bossStages.Length)];
            }
            while (secondStage == firstStage);
            //Делаем так чтобы предыдущие две стадии и текущие не были одинаковыми
            if(firstStage == previousFirstStage && secondStage == previousSecondStage || firstStage == previousSecondStage && secondStage == previousFirstStage)
            {
                continue;
            }
            previousFirstStage = firstStage;
            previousSecondStage = secondStage;
            
            SetGameState(firstStage);
            SetGameState(secondStage);
            yield return new WaitForSeconds(24f);
        }
    }

    

    
    public void StartGoodEnding()
    {
        isGoodEndingActive = true;
        StartCoroutine(GoodEnding());
    }
    public void StartBadEnding()
    {
        isBadEndingActive = true;
        StartCoroutine(BadEnding());
    }
    private IEnumerator GoodEnding()
    {
        //Запускаем анимацию финального удара по церберу
        bodyAndHeads.GetComponent<Animator>().SetTrigger("Death");
        AudioManager.instance.PlaySoundFXClip(finalBlowSound, transform, 0.6f);
        yield return new WaitForSeconds(1f);
        hellhoundEffectsAnimator.SetTrigger("Death");
        //Цербер падает на колени
        yield return new WaitForSeconds(2.9f);
        //Меняем выражение лиц
        mops.GetComponent<Animator>().SetBool("Damage", true);
        chih.GetComponent<Animator>().SetBool("Damage", true);
        bul.GetComponent<Animator>().SetBool("Damage", true);
        AudioManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
        
        yield return new WaitForSeconds(3f);
        //Запускаем первый диалог
        SetGameState(GameState.GoodEndingDialogue1);
        yield return new WaitUntil(() => dialogueManager.dialogues[6].isPassed);
        //Падение азбуки
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        SetGameState(GameState.ABC);
        yield return new WaitUntil(() => playerController.hasABC);
        //Запускаем второй диалог
        SetGameState(GameState.GoodEndingDialogue2);
        yield return new WaitUntil(() => dialogueManager.dialogues[7].isPassed);
        //Уход лягушки
        lowerLimit.SetActive(false);
        InputManager.PlayerInput.SwitchCurrentActionMap("Cutscene");
        float elapsedTime = 0f;
        playerController.animator.SetTrigger("Walk Away");
        AudioManager.instance.PlaySoundFXClip(walkingSound, transform, 1f, 4f);
        while (elapsedTime < 4f)
        {
            elapsedTime += Time.deltaTime;
            playerController.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 5000f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        //Запускаем третий диалог
        SetGameState(GameState.GoodEndingDialogue3);
        yield return new WaitUntil(() => dialogueManager.dialogues[8].isPassed);
        SceneManager.LoadScene("End");

    }

    private IEnumerator BadEnding()
    {
        //Меняем выражение лиц
        mops.GetComponent<Animator>().SetBool("70hp", false);
        chih.GetComponent<Animator>().SetBool("15hp", false);
        bul.GetComponent<Animator>().SetBool("40hp", false);
        //Запускаем анимацию финальногЫо удара по лягушке
        AudioManager.instance.PlaySoundFXClip(vingetteSound, transform, 0.4f);
        yield return new WaitForSeconds(1f);
        vignette.SetActive(true);
        playerAnimator.SetTrigger("Death");
        playerEffectsAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(2.9f);
        AudioManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
        yield return new WaitForSeconds(4f);
        //Запускаем диалог
        SetGameState(GameState.BadEndingDialogue);
        yield return new WaitUntil(() => dialogueManager.dialogues[9].isPassed);
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
        //Включаем меню смерти
        deathMenu.gameObject.SetActive(true); 
    }

    private IEnumerator SmoothMusicShutdown()
    {
        float elapsedTime = 0f;
        while(elapsedTime < 3f)
        {
            mainThemeSource.volume = Mathf.Lerp(mainThemeSource.volume, 0f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainThemeSource.Stop();
    }
   

    
    public void GameOver()
    {
        //Останавливаем все корутины менеджера
        StopAllCoroutines();
        //Останавливаем все звуки кроме главной темы
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source != mainThemeSource)
            {
                source.Stop();
            }
        }
        //Заглушаем главную тему
        StartCoroutine(SmoothMusicShutdown());
        //Останавливаем всех собак

        //Мопс
        if(mopsStage.wind.activeSelf)
        {
            mopsStage.wind.SetActive(false);
        }
        mopsStage.fireAboveHead.SetActive(false);
        mopsStage.StopAllCoroutines();
        //Чих
        if (chihStage.activeTilemap != null)
        {
            Destroy(chihStage.activeTilemap.gameObject);
        }
        chihStage.fireAboveHead.SetActive(false);
        chihStage.StopAllCoroutines();
        //Буль
        if(bulStage.portalSpriteRenderer.enabled)
        {
            bulStage.portalSpriteRenderer.enabled = false;
        }
        bulStage.fireAboveHead.SetActive(false);
        bulStage.StopAllCoroutines();

        //Если диалоговое окно или листок активны, выключаем
        if (dialogueElements.activeSelf)
        {
            dialogueManager.StopAllCoroutines();
            dialogueElements.SetActive(false);
        }
        if (leaflet.activeSelf)
        {
            leaflet.SetActive(false);
        }
        greenFire.SetActive(false);
        //Переводим управление на карту Cutscene
        InputManager.PlayerInput.SwitchCurrentActionMap("Cutscene");

    }

}
