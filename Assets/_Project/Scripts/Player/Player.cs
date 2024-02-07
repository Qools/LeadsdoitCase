using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _brakeButton;
    [SerializeField] private Button _throttleButton;

    [SerializeField] private List<Sprite> _sprites;


    [SerializeField] private float _maxSpeed;
    private float _speed;
    [SerializeField] private float sideClampX;
    private float _direction;
    public bool isShieldActive;
    [SerializeField] private bool _isMagnetActive;

    [SerializeField] private float _magnetRadius = 7f;

    [SerializeField] private float _nitorSpeed;

    private int _coinAmount;
    [SerializeField] private int _coinValue;

    private int _hp;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _hpValue;

    [SerializeField] private int _crackDamage;

    [SerializeField] private int _oilDamage;
    [SerializeField] private float _oilSpeedSlowDown;
    [SerializeField] private float _oilSlowDownTime;

    [SerializeField] private float _powerUpsDuration;

    // Start is called before the first frame update
    void Start()
    {
        _setControlButtons();
        _disableMagnet();

        isShieldActive = false;
        _isMagnetActive = false;

        _hp = 100;
        EventSystem.CallHpBarChange(_hp);

        _coinAmount = 0;

        GameController.Instance.playerTransform = this.transform;
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

        if (_isMagnetActive)
        {
            _coinMagnet();
        }
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += _onGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= _onGameOver;
    }

    private void _movePlayer()
    {
        Vector3 direction = new Vector3(_direction, 1f, 0f);
        this.transform.Translate(direction.normalized * _speed * Time.deltaTime);

        Vector3 tempPos = this.transform.position;
        tempPos.x = Mathf.Clamp(tempPos.x, -sideClampX, sideClampX);
        this.transform.position = tempPos;
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
                _enableMagnet(other.gameObject);
            break;

            case PlayerPrefKeys.NITRO:
                _enableNitro(other.gameObject);
            break;

            case PlayerPrefKeys.HP:
                _addHp(other.gameObject);
            break;

            case PlayerPrefKeys.SHIELD:
                _enableShield(other.gameObject);
            break;

            case PlayerPrefKeys.OIL:
                _hitOil();
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

    private void _enableMagnet(GameObject _magnetGameObject)
    {
        _changeCarSprite(1);

        _isMagnetActive = true;

        EventSystem.CallMagnetBarChange(0, _powerUpsDuration);

        DOVirtual.DelayedCall(_powerUpsDuration, () => _disableMagnet());

        _magnetGameObject.SetActive(false);
    }

    private void _disableMagnet()
    {
        _isMagnetActive = false;

        _changeCarSprite(0);
    }

    private void _enableNitro(GameObject _nitroGameObject)
    {
        _changeCarSprite(2);

        _speed = _nitorSpeed;

        EventSystem.CallNitroBarChange(0, _powerUpsDuration);

        DOVirtual.DelayedCall(_powerUpsDuration, () => _disableNitro());

        _nitroGameObject.SetActive(false);
    }

    private void _disableNitro()
    {
        _speed = _maxSpeed;

        _changeCarSprite(0);
    }

    private void _enableShield(GameObject _shieldGameObject)
    {
        _changeCarSprite(3);

        isShieldActive = true;

        EventSystem.CallShieldBarChange(0, _powerUpsDuration);

        DOVirtual.DelayedCall(_powerUpsDuration, () => _disableShield());

        _shieldGameObject.SetActive(false);
    }

    private void _disableShield()
    {
        isShieldActive = false;

        _changeCarSprite(0);
    }

    private void _hitOil()
    {
        if (isShieldActive)
        {
            return;
        }

        _changeHP(-_crackDamage);

        _speedSlowDown();
    }

    private void _hitCrack()
    {
        if (isShieldActive)
        {
            return;
        }

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
        }
    }

    private void _speedSlowDown()
    {
        _speed = _oilSpeedSlowDown;

        DOVirtual.DelayedCall(_oilSlowDownTime, ()=> _speed = _maxSpeed);
    }

    private void _changeCarSprite(int _id)
    {
        _spriteRenderer.sprite = _sprites[_id];
    }

    private void _onGameOver(GameResult gameResult)
    {
        _speed = 0f;
    }

    private void _coinMagnet()
    {
        var hitColliders = Physics2D.OverlapCircleAll(this.transform.position, _magnetRadius);

        foreach (var item in hitColliders)
        {
            if (item.TryGetComponent(out CoinPowerUp coin))
            {
                coin.ActivateMagnetMove();
            }
        }
    }
}
