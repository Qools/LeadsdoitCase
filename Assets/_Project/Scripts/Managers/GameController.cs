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

    private GameObject _spawnedPolice;
    private bool _isPoliceSpawned;    
    private float _spawnTimer;
    private float _despawnTimer;
    [SerializeField] private float _spawnDistanceToPlayer;
    [SerializeField] private float _policeSpawnTime;
    [SerializeField] private float _policeDespawnTime;
        
    public void Init()
    { 
        SetStatus(Status.ready);
    }

    private void Start()
    {
        _score = 0f;
        _spawnTimer = 0f;
        _despawnTimer = 0f;

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
            _spawnTimer += Time.deltaTime;
        }

        else
        {
            _despawnTimer += Time.deltaTime;
        }

        if (_spawnTimer >= _policeSpawnTime) 
        {
            _spawnPolice();
        }

        if (_despawnTimer >= _policeDespawnTime)
        {
            if (!_isPoliceSpawned)
            {
                return;
            }

            _despawnPolice();
        }

        if (playerTransform != null)
        {
            _score = playerTransform.position.y;
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
        _despawnTimer = 0f;
        _spawnTimer = 0f;
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
        _spawnTimer = 0f;
        _despawnTimer = 0f;

        int random = Random.Range(0, _policeCarPrefabs.Count);

        GameObject _spawnedPoliceCar = Instantiate(
            _policeCarPrefabs[random], 
            playerTransform.position - new Vector3(0f, _spawnDistanceToPlayer, 0f),
            Quaternion.identity,
            GameManager.Instance.currentLevel.transform);

        _spawnedPolice = _spawnedPoliceCar;

        if (_spawnedPoliceCar.TryGetComponent(out PoliceCar _policeCar))
        {
            _policeCar.SetPlayer(playerTransform);
        }
    }

    private void _despawnPolice()
    {
        _isPoliceSpawned = false;

        _despawnTimer = 0f;

        if (_spawnedPolice.TryGetComponent(out PoliceCar policeCar))
        {
            policeCar.StopPolice();
        }

        DOVirtual.DelayedCall(3f, () =>
        {
            Destroy(_spawnedPolice);
            _isPoliceSpawned = false;
        });
    }
}
