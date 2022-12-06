using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeerChangeOperatCharacter : ChangeOperatCharacter
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

        _isReadyChange = result;

        return result && Input.GetButtonDown(_changeButtonName);
    }
}
