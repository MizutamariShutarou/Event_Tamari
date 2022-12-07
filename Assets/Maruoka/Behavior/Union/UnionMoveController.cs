using System;
using UnityEngine;

[System.Serializable]
public class UnionMoveController : MoveBehavior
{
    private UnionStateController _stateController = null;

    public void Init(Rigidbody2D rigidbody2D,UnionStateController stateController)
    {
        _rb2D = rigidbody2D;
        _stateController = stateController;
    }

    protected override bool IsRun()
    {
        bool result = false;

        result =
            _stateController.CurrentState == UnionState.IDLE ||
            _stateController.CurrentState == UnionState.MOVE;

        return result;
    }
}