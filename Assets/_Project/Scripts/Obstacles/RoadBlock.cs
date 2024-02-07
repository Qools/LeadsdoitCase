using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerPrefKeys.PLAYER))
        {
            EventSystem.CallGameOver(GameResult.Lose);
        }
    }
}
