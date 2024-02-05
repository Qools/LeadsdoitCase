using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    [SerializeField] private Transform roadEndPoint;

    public Vector3 GetRoadEndPointPosition()
    {
        return roadEndPoint.position;
    }

    public void CallDestroyRoadPart()
    {
        DOVirtual.DelayedCall(10f, () => _destroyRoadPart());
    }

    private void _destroyRoadPart()
    {
        Destroy(this.gameObject);
    }
}
