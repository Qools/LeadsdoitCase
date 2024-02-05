using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup startMenu;
    [SerializeField] private CanvasGroup endGameMenu;
    [SerializeField] private CanvasGroup inGamePanel;

    private void Start()
    {
        startMenu.DOFade(1, 0.01f);
        startMenu.interactable = true;
        startMenu.blocksRaycasts = true;

        endGameMenu.DOFade(0, 0.01f);
        endGameMenu.interactable = false;
        endGameMenu.blocksRaycasts = false;

        inGamePanel.DOFade(0, 0.01f);
        inGamePanel.interactable = false;
        inGamePanel.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        EventSystem.OnStartGame += OnGameStart;
        EventSystem.OnGameOver += OnGameEnd;
        EventSystem.OnNewLevelLoad += OnNewLevelLoad;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= OnGameStart;
        EventSystem.OnGameOver -= OnGameEnd;
        EventSystem.OnNewLevelLoad -= OnNewLevelLoad;
    }

    private void OnGameStart()
    {
        startMenu.DOFade(0, 0.01f);
        startMenu.interactable = false;
        startMenu.blocksRaycasts = false;

        endGameMenu.DOFade(0, 0.01f);
        endGameMenu.interactable = false;
        endGameMenu.blocksRaycasts = false;

        inGamePanel.DOFade(1, 0.01f);
        inGamePanel.interactable = true;
        inGamePanel.blocksRaycasts = true;
    }

    private void OnGameEnd(GameResult gameResult)
    {
        startMenu.DOFade(0, 0.01f);
        startMenu.interactable = false;
        startMenu.blocksRaycasts = false;

        endGameMenu.DOFade(1, 0.01f);
        endGameMenu.interactable = true;
        endGameMenu.blocksRaycasts = true;

        inGamePanel.DOFade(0, 0.01f);
        inGamePanel.interactable = false;
        inGamePanel.blocksRaycasts = false;
    }

    private void OnNewLevelLoad()
    {
        startMenu.DOFade(1, 0.01f);
        startMenu.interactable = true;
        startMenu.blocksRaycasts = true;

        endGameMenu.DOFade(0, 0.01f);
        endGameMenu.interactable = false;
        endGameMenu.blocksRaycasts = false;

        inGamePanel.DOFade(0, 0.01f);
        inGamePanel.interactable = false;
        inGamePanel.blocksRaycasts = false;
    }
}
