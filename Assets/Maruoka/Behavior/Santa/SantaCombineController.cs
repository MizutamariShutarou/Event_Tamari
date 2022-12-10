using System;
using UnityEngine;

[System.Serializable]
public class SantaCombineController : CombineController
{
    private SantaStateController _stateController = null;

    public void Init(SantaStateController stateController)
    {
        _stateController = stateController;
    }

    protected override bool IsRun()
    {
        bool result = false;

        result =
            _isReadyCanCombine &&
             (_stateController.CurrentState == SantaState.IDLE ||
             _stateController.CurrentState == SantaState.MOVE);

        _isReadyCanCombine = result;

        return result && Input.GetButtonDown(_combineButtonName);
    }
}