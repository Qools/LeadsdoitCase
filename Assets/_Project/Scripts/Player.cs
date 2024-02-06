using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _brakeButton;
    [SerializeField] private Button _throttleButton;

    [SerializeField] private Collider2D _magnetCollider;

    [SerializeField] private float _maxSpeed;
    private float _speed;
    private float _direction;

    private int _coinAmount;
    [SerializeField] private int _coinValue;

    private int _hp;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _hpValue;

    [SerializeField] private int _crackDamage;

    [SerializeField] private int _oilDamage;
    [SerializeField] private float _oilSpeedSlowDown;
    [SerializeField] private float _oilSlowDownTime;


    // Start is called before the first frame update
    void Start()
    {
        _setControlButtons();
        _disableMagnet();

        _hp = 100;
        EventSystem.CallHpBarChange(_hp);

        _coinAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            _leftClicked();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _rightClicked();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _throttleClicked();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _brakeClicked();
        }
#endif
        _movePlayer();
    }

    private void _movePlayer()
    {
        this.transform.Translate(new Vector3(_direction, 1f, 0f) * _speed * Time.deltaTime);
    }

    private void _throttleClicked()
    {
        _speed = _maxSpeed;
    }

    private void _brakeClicked()
    {
        _speed = 0f;
    }

    private void _leftClicked()
    {
        _direction = -1;

        DOTween.To(x => _direction = x, -1f, 0f, 1f);
    }

    private void _rightClicked()
    {
        _direction = 1;

        DOTween.To(x => _direction = x, 1f, 0f, 1f);
    }

    public Vector3 GetPoisition() 
    { 
        return this.transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case PlayerPrefKeys.COIN:
                _addCoin(other.gameObject);
            break;

            case PlayerPrefKeys.MAGNET:
                _enableMagnet();
            break;

            case PlayerPrefKeys.NITRO:
                _enableNitro();
            break;

            case PlayerPrefKeys.HP:
                _addHp(other.gameObject);
            break;

            case PlayerPrefKeys.SHIELD:
                _enableShield();
            break;

            case PlayerPrefKeys.OIL:
                _hitOil();
            break;

            case PlayerPrefKeys.BARRIER:
                _hitBarrier();
            break;

            case PlayerPrefKeys.CRACK:
                _hitCrack();
            break;
        }
    }

    private void _setControlButtons()
    {
        _leftButton.onClick.AddListener(delegate { _leftClicked(); });
        _rightButton.onClick.AddListener(delegate { _rightClicked(); });
        _brakeButton.onClick.AddListener(delegate { _brakeClicked(); });
        _throttleButton.onClick.AddListener(delegate { _throttleClicked(); });
    }

    private void _addCoin(GameObject _coinGameObject)
    {
        _coinAmount += _coinValue;

        _coinGameObject.SetActive(false);
    }

    private void _addHp(GameObject _hpGameObject)
    {
        if (_hp <= _maxHp)
        {
            _changeHP(_hpValue);
        }

        _hpGameObject.SetActive(false);
    }

    private void _enableMagnet()
    {
        _magnetCollider.enabled = true;
    }

    private void _disableMagnet()
    {
        _magnetCollider.enabled = false;
    }

    private void _enableNitro()
    {

    }

    private void _enableShield()
    {

    }

    private void _hitBarrier()
    {
        EventSystem.CallGameOver(GameResult.Lose);

        _speed = 0f;
    }

    private void _hitOil()
    {
        _changeHP(-_crackDamage);

        _speedSlowDown();
    }

    private void _hitCrack()
    {
        _changeHP(-_crackDamage);
    }

    private void _changeHP(int hpValue)
    {
        _hp += hpValue;

        EventSystem.CallHpBarChange(_hp);

        _checkHp();
    }

    private void _checkHp()
    {
        if (_hp <= 0)
        {
            EventSystem.CallGameOver(GameResult.Lose);

            _speed = 0f;
        }
    }

    private void _speedSlowDown()
    {
        _speed = _oilSpeedSlowDown;

        DOVirtual.DelayedCall(_oilSlowDownTime, ()=> _speed = _maxSpeed);
    }
}
