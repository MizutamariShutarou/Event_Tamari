using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
          Input.GetButtonDown(_changeButtonName) &&
          (_stateController.CurrentState == DeerState.IDLE ||
          _stateController.CurrentState == DeerState.MOVE);

        return result;
    }
}
