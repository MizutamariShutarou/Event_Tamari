using System;
using UnityEngine;

[System.Serializable]
public class DeerMoveController : MoveBehavior
{
    private DeerStateController _stateController = null;

    public void Init(Rigidbody2D rigidbody2D, DeerStateController stateController)
    {
        _rb2D = rigidbody2D;
        _stateController = stateController;
    }

    protected override bool IsRun()
    {
        bool result = false;

        result =
            _stateController.CurrentState == DeerState.IDLE ||
            _stateController.CurrentState == DeerState.MOVE;

        return result;
    }
}