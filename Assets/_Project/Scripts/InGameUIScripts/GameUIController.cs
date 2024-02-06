using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
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
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        _setScoreText();
    }

    private void OnEnable()
    {
        EventSystem.OnStartGame += _enableGameUI;
        EventSystem.OnGameOver += _enableGameOverPanel;

        EventSystem.OnHpBarChange += _onHpBarChange;
        EventSystem.OnMagnetBarChange += _onMagnetBarChange;
        EventSystem.OnNitroBarChange += _onNitroBarChange;
        EventSystem.OnShieldBarChange += _onShieldBarChange;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= _enableGameUI;
        EventSystem.OnGameOver -= _enableGameOverPanel;

        EventSystem.OnHpBarChange -= _onHpBarChange;
        EventSystem.OnMagnetBarChange -= _onMagnetBarChange;
        EventSystem.OnNitroBarChange -= _onNitroBarChange;
        EventSystem.OnShieldBarChange -= _onShieldBarChange;
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

    private void _onHpBarChange(int _hpValue)
    {
        _hpBar.DOValue(_hpValue, 1f);
    }

    private void _onMagnetBarChange(int _magnetValue, float _duration)
    {
        _enableBar(_magnetBar, true);

        _magnetBar.value = _magnetBar.maxValue;

        _magnetBar.DOValue(_magnetValue, _duration).OnComplete(()=>
        {
            _enableBar(_magnetBar, false);
        });
    }

    private void _onNitroBarChange(int _nitroValue, float _duration)
    {
        _enableBar(_nitroBar, true);

        _nitroBar.value = _nitroBar.maxValue;

        _nitroBar.DOValue(_nitroValue, _duration).OnComplete(() =>
        {
            _enableBar(_nitroBar, false);
        });
    }

    private void _onShieldBarChange(int _shieldValue, float _duration)
    {
        _enableBar(_shieldBar, true);

        _shieldBar.value = _shieldBar.maxValue;

        _shieldBar.DOValue(_shieldValue, _duration).OnComplete(() =>
        {
            _enableBar(_shieldBar, false);
        });
    }

    private void _enableBar(Slider _bar, bool isEnabled)
    {
        _bar.gameObject.SetActive(isEnabled);
    }

    private void _setScoreText()
    {
        _scoreText.text = PlayerPrefKeys.SCORE + " " + GameController.Instance.GetScore().ToString();
    }
}
