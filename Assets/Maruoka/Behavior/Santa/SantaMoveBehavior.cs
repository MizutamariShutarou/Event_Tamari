using System;
using UnityEngine;

[System.Serializable]
public class SantaMoveBehavior : MoveBehavior, ICrawlingBehavior
{
    private bool _isCreeping = false;

    [SerializeField, Range(0.0f, 1.0f)]
    float _crawlingSpeed = 0.5f;

    public override void Move()
    {
        if (_isMove)
        {
            var h = Input.GetAxisRaw(_horizontalButtonName);
            h *= _isCreeping ? _crawlingSpeed : 1.0f; // ô≥ô¥çsìÆÇµÇƒÇ¢ÇÈèÍçáÇÕå∏ë¨Ç∑ÇÈÅB
            _rb2D.velocity = new Vector2(h * _moveSpeed, _rb2D.velocity.y);
        }
    }

    public void CreepingEnter()
    {
        _isCreeping = true;
    }

    public void CreepingExit()
    {
        _isCreeping = false;
    }
}