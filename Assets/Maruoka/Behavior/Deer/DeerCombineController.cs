using System;
using UnityEngine;

[System.Serializable]
public class DeerCombineController : CombineController
{
    private DeerStateController _stateController = null;

    public void Init(DeerStateController stateController)
    {
        _stateController = stateController;
    }

    protected override bool IsRun()
    {
        bool result = false;

        result =
             (_stateController.CurrentState == DeerState.IDLE ||
             _stateController.CurrentState == DeerState.MOVE);
        _isReadyCanCombine = result;

        return result && Input.GetButtonDown(_combineButtonName);
    }
}