using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private const float _PLAYER_DISTANCE_TO_SPAWN_ROAD = 100f;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _start;
    [SerializeField] private List<Transform> _roadParts;

    [SerializeField] private Player _player;

    private Vector3 _lastEndPointPosition;

    [SerializeField] private int _startRoadPartsCount = 5;

    private void Awake()
    {
        if(_start.TryGetComponent(out RoadPart _roadPart))
        {
            _lastEndPointPosition = _roadPart.GetRoadEndPointPosition();
            _roadPart.CallDestroyRoadPart();
        }

        for (int i = 0; i < _startRoadPartsCount; i++)
        {
            _spanwRoadPart();
        }
    }

    private void Update()
    {

        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        _checkPlayerDistance();
    }

    private void _spanwRoadPart()
    {
        int roadPartId = Random.Range(0, _roadParts.Count);

        Transform lastSpawnedRoadTransform = _spanwRoadPart(roadPartId, _lastEndPointPosition);
        
        if (lastSpawnedRoadTransform.TryGetComponent(out RoadPart _roadPart))
        {
            _lastEndPointPosition = _roadPart.GetRoadEndPointPosition();
            _roadPart.CallDestroyRoadPart();
        }
    }

    private Transform _spanwRoadPart(int roadPartId, Vector3 spawnPosition)
    {
        Transform roadPart = Instantiate(_roadParts[roadPartId], spawnPosition, Quaternion.identity, _parent);
        return roadPart;
    }

    private void _checkPlayerDistance()
    {
        if (Vector3.Distance(_player.GetPoisition(), _lastEndPointPosition) < _PLAYER_DISTANCE_TO_SPAWN_ROAD)
        {
            _spanwRoadPart();
        }
    }
}
