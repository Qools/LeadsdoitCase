using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Transform playerTransform;

    private float _score;
        
    public void Init()
    { 
        SetStatus(Status.ready);
    }

    private void Start()
    {
        _score = 0f;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        if (playerTransform != null)
        {
            _score += playerTransform.localPosition.y * Time.deltaTime;
        }
    }


    private void OnEnable()
    {
        EventSystem.OnNewLevelLoad += OnNewLevelLoad;

        EventSystem.OnGameOver += _onGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnNewLevelLoad -= OnNewLevelLoad;

        EventSystem.OnGameOver -= _onGameOver;
    }

    private void OnNewLevelLoad()
    {
        DOTween.KillAll();

        _score = 0f;
    }

    private void _onGameOver(GameResult gameResult)
    {
        if (_score >= GetHighScore())
        {
            _setHighScore(_score);
        }
    }

    public int GetScore()
    {
        return Mathf.RoundToInt(_score);
    }

    public int GetHighScore()
    {
        int _highScore = 0;

        if (!PlayerPrefs.HasKey(PlayerPrefKeys.HIGHSCORE))
        {
            _setHighScore(_highScore);
        }

        else
        {
            _highScore = PlayerPrefs.GetInt(PlayerPrefKeys.HIGHSCORE);
        }

        return _highScore;
    }

    private void _setHighScore(float highScore)
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.HIGHSCORE, Mathf.RoundToInt(highScore));
    }
}
