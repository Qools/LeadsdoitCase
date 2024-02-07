using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private GameObject _lightningEffect;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    private Transform _playerTransform;

    private bool _isPlayerHit;

    private void Start()
    {
        _isPlayerHit = false;
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
            _minSpeed = 0f;
            _maxSpeed = 0f;

            _isPlayerHit = true;

            EventSystem.CallGameOver(GameResult.Lose);

            Instantiate(_lightningEffect, this.transform.position + new Vector3(0f, 2f), Quaternion.identity, this.transform);
        }
    }

}
