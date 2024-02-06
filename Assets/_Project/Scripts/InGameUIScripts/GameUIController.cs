using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : Singleton<GameUIController>
{
    [SerializeField] private CanvasGroup _uiCanvasGroup;

    [SerializeField] private Button _pauseButton;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _magnetBar;
    [SerializeField] private Slider _nitroBar;
    [SerializeField] private Slider _shieldBar;

    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private TextMeshProUGUI _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _disableBars();
        _disableGameUI();
        _disablePanels();

        _pauseButton.onClick.AddListener(delegate { _openPauseMenu(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.OnStartGame += _enableGameUI;
        EventSystem.OnGameOver += _enableGameOverPanel;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= _enableGameUI;
        EventSystem.OnGameOver -= _enableGameOverPanel;
    }

    private void _disableBars()
    {
        _magnetBar.gameObject.SetActive(false);
        _nitroBar.gameObject.SetActive(false);
        _shieldBar.gameObject.SetActive(false);
    }

    private void _enableGameUI()
    {
        _uiCanvasGroup.alpha = 1;
        _uiCanvasGroup.interactable = true;
        _uiCanvasGroup.blocksRaycasts = true;
    }

    private void _disableGameUI()
    {
        _uiCanvasGroup.alpha = 0;
        _uiCanvasGroup.interactable = false;
        _uiCanvasGroup.blocksRaycasts = false;
    }

    private void _disablePanels()
    {
        _pauseMenuPanel.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(false);
    }

    private void _openPauseMenu()
    {
        _pauseMenuPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        _pauseMenuPanel.SetActive(false);

        Time.timeScale = 1;
    }

    private void _enableGameOverPanel(GameResult gameResult)
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
}
