using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public GameObject _effect;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector3 _punchScale;
    [SerializeField] private float _punchEffectDuration;

    Sequence _sequence;

    public void ChangeEffectSprite(Sprite _sprite, float _duration, bool _isNitro)
    {
        if (_isNitro)
        {
            _effect.transform.localPosition = new Vector2(0f, -3f);
        }

        else
        {
            _effect.transform.localPosition = Vector2.zero;
        }

        _enableEffect(true);
        _scaleEffect();

        _spriteRenderer.sprite = _sprite;

        if (_effect.activeSelf)
        {
            if ( _sequence == null) { return; }

            _sequence.Kill();
            _sequence.Append( DOVirtual.DelayedCall(_duration, () => _enableEffect(false)) );
        }

    }

    private void _enableEffect(bool _isEnable)
    {
        _effect.SetActive(_isEnable);
    }

    private void _scaleEffect()
    {
        _effect.transform.DOScale(_punchScale, _punchEffectDuration).SetLoops(-1, LoopType.Yoyo);
    }
}
