using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SantaChangeOperatCharacter : ChangeOperatCharacter
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
          (_stateController.CurrentState == SantaState.IDLE ||
          _stateController.CurrentState == SantaState.MOVE);

        _isReadyChange = result;

        return result && Input.GetButtonDown(_changeButtonName);
    }
}
