using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private GameObject _lightningEffect;

    [SerializeField] private SpriteRenderer _effect;
    [SerializeField] private List<Sprite> _glows;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    private Transform _playerTransform;

    private bool _isPlayerHit;

    private void Start()
    {
        _isPlayerHit = false;

        _changeEffect();
    }

    private void Update()
    {
        if (_playerTransform == null)
        {
            return;
        }

        if (_isPlayerHit)
        {
            return;
        }

        _movePoliceCar();
    }

    private void _movePoliceCar()
    {
        float randomSpeed = Random.Range(_minSpeed, _maxSpeed);

        Vector3 distance = _playerTransform.position - this.transform.position;
        this.transform.Translate(distance.normalized * randomSpeed * Time.deltaTime);
    }

    public void SetPlayer(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerPrefKeys.PLAYER))
        {
            if (collision.TryGetComponent(out Player _player))
            {
                if (_player.GetShildStatus())
                {
                    return;
                }
            }

            StopPolice();

            EventSystem.CallGameOver(GameResult.Lose);

            Instantiate(_lightningEffect, this.transform.position + new Vector3(0f, 2f), Quaternion.identity, this.transform);
        }
    }

    public void StopPolice()
    {
        _minSpeed = 0f;
        _maxSpeed = 0f;

        _isPlayerHit = true;
    }

    private void _changeEffect()
    {
        _effect.sprite = _glows[Random.Range(0, _glows.Count)];

        DOVirtual.DelayedCall(0.5f, () => _changeEffect());
    }
}
