using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LevelList levelList;

    [HideInInspector]
    public GameObject currentLevel;

    public bool isGameStarted = false;

    private IEnumerator Start()
    {
        yield return StartCoroutine(DataManager.Instance.WaitInit());

        yield return StartCoroutine(MenuManager.Instance.WaitInit(MenuManager.Instance.Init));

        yield return StartCoroutine(GameController.Instance.WaitInit(GameController.Instance.Init));

        yield return StartCoroutine(WaitInit(Init));

        LoadLevel(DataManager.Instance.GetLevel());

        MenuManager.Instance.loadingScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //GameOver();
        }
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += GameResult;
        EventSystem.OnStartGame += OnGameStarted;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= GameResult;
        EventSystem.OnStartGame -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        isGameStarted = true;
    }

    private void GameResult(GameResult gameResult)
    {
        isGameStarted = false;

        if (gameResult == global::GameResult.Win)
        {
            Win();
        }
    }

    public void Init()
    {
        SetStatus(Status.ready);
    }

    public void LoadLevel(int _level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelList.LoopLevelsByIndex(_level));

        EventSystem.CallNewLevelLoad();

        MenuManager.Instance.SwitchPanel<StartMenu>();     
    }

    public void Win()
    {
        DataManager.Instance.SetLevel(DataManager.Instance.GetLevel() + 1);
    }
}
