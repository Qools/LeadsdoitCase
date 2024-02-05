using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameMenu : UIPanel
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    public TextMeshProUGUI endGameText;

    private void Start()
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        EventSystem.OnGameOver += OnGameEnd;
    }

    public void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameEnd;
    }

    private void OnGameEnd(GameResult gameResult)
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        if (gameResult == GameResult.Win)
        {
            Win();
        }

        else
        {
            GameOver();
        }
        
    }

    private void Win()
    {
        nextLevelButton.gameObject.SetActive(true);

        endGameText.text = "Level " + (DataManager.Instance.GetLevel() - 1).ToString() + " Completed";
    }

    private void GameOver()
    {
        retryButton.gameObject.SetActive(true);

        endGameText.text = "Level Failed";
    }

    public void OnNextButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());

        MenuManager.Instance.SwitchPanel<StartMenu>();
    }

    public void OnRetryButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());

        MenuManager.Instance.SwitchPanel<StartMenu>();
    }
}
