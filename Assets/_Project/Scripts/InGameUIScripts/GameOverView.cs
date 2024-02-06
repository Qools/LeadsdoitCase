using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    void Start()
    {
        _setButtons();
    }

    private void _setButtons()
    {
        _restartButton.onClick.AddListener(delegate { GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel()); });
        _exitButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}
