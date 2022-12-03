using System;
using UnityEngine;

[System.Serializable]
public class SantaMoveBehavior : MoveBehavior, ICrawlingBehavior
{
    private bool _isCreepingNow = false;

    [SerializeField, Range(0.0f, 1.0f)]
    float _crawlingSpeed = 0.5f;

    public bool IsCreeping => _isCreepingNow;

    public override void Update()
    {
        if (IsRun())
        {
            var h = Input.GetAxisRaw(_horizontalButtonName);
            h *= _isCreepingNow ? _crawlingSpeed : 1.0f; // ô≥ô¥çsìÆÇµÇƒÇ¢ÇÈèÍçáÇÕå∏ë¨Ç∑ÇÈÅB
            _rb2D.velocity = new Vector2(h * _moveSpeed, _rb2D.velocity.y);
        }
    }

    public void CreepingEnter()
    {
        _isCreepingNow = true;
    }

    public void CreepingExit()
    {
        _isCreepingNow = false;
    }

    private SantaStateController _stateController = null;
    public void Init(Rigidbody2D rigidbody2D, SantaStateController stateController)
    {
        _rb2D = rigidbody2D;
        _stateController = stateController;
    }

    protected override bool IsRun()
    {
        bool result = false;

        result =
            _stateController.CurrentState == SantaState.IDLE ||
            _stateController.CurrentState == SantaState.MOVE;

        return result;
    }
}