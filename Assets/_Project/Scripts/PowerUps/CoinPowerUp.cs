using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : MonoBehaviour
{
    Transform _playerTransform;
    private bool _isMagnetActivated = false;


    public void ActivateMagnetMove()
    {
        _playerTransform = GameController.Instance.playerTransform;

        _isMagnetActivated = true;
    }

    private void Update()
    {
        if (!_isMagnetActivated)
        {
            return;
        }

        _moveToPlayer();
    }

    private void _moveToPlayer()
    {
        Vector3 distance = _playerTransform.position - this.transform.position;
        this.transform.Translate(distance.normalized * 15f * Time.deltaTime);
    }
}
