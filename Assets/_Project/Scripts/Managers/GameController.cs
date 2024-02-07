using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Transform playerTransform;

    [SerializeField] private List<GameObject> _policeCarPrefabs;

    private float _score;

    private bool _isPoliceSpawned;    
    private float _timer;
    [SerializeField] private float _spawnDistanceToPlayer;
    [SerializeField] private float _policeSpawnTime;
        
    public void Init()
    { 
        SetStatus(Status.ready);
    }

    private void Start()
    {
        _score = 0f;
        _timer = 0f;

        _isPoliceSpawned = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        if (!_isPoliceSpawned)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _policeSpawnTime) 
        {
            _spawnPolice();
        }

        if (playerTransform != null)
        {
            _score = playerTransform.position.y /** Time.deltaTime*/;
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

        _isPoliceSpawned = false;
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

    private void _spawnPolice()
    {
        _isPoliceSpawned = true;
        _timer = 0f;

        int random = Random.Range(0, _policeCarPrefabs.Count);

        GameObject _spawnedPoliceCar = Instantiate(
            _policeCarPrefabs[random], 
            playerTransform.position - new Vector3(0f, _spawnDistanceToPlayer, 0f),
            Quaternion.identity,
            GameManager.Instance.currentLevel.transform);

        if (_spawnedPoliceCar.TryGetComponent(out PoliceCar _policeCar))
        {
            _policeCar.SetPlayer(playerTransform);
        }
    }
}
