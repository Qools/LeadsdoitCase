using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerPrefKeys.MAGNET_COLLIDER))
        {
            this.transform.DOMove(collision.transform.position, 1f);
        }
    }
}
