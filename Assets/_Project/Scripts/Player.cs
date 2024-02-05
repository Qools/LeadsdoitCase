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

    [SerializeField] private float _maxSpeed;
    private float _speed;
    private float _direction;

    // Start is called before the first frame update
    void Start()
    {
        _leftButton.onClick.AddListener(delegate { _leftClicked(); });
        _rightButton.onClick.AddListener(delegate { _rightClicked(); });
        _brakeButton.onClick.AddListener(delegate { _brakeClicked(); });
        _throttleButton.onClick.AddListener(delegate { _throttleClicked(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

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

        _movePlayer();
    }

    private void _movePlayer()
    {
        this.transform.Translate(new Vector3(_direction, 1f, 0f) * _speed * Time.deltaTime);
    }

    private void _throttleClicked()
    {
        _speed = 0f;
    }

    private void _brakeClicked()
    {
        _speed = _maxSpeed;
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
}
