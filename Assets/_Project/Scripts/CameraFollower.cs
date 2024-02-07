using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 5f, -25f);
    Vector3 currentVelocity;
    public float smoothTime = 0.25f;

    private void OnEnable()
    {
        EventSystem.OnStartGame += _onGameStarted;
        EventSystem.OnNewLevelLoad += _onNewLevelLoaded;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= _onGameStarted;
        EventSystem.OnNewLevelLoad -= _onNewLevelLoaded;
    }

    private void _onNewLevelLoaded()
    {
        this.transform.position = _offset;
    }

    private void _onGameStarted()
    {
        _playerTransform = GameController.Instance.playerTransform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        Vector3 target = new Vector3(0f, _playerTransform.position.y, 0f);

        transform.position = Vector3.SmoothDamp(
         transform.position,
         target + _offset,
         ref currentVelocity,
         smoothTime
         );
    }
}
