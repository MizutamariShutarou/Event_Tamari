using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeerMainLifeController : MainLifeController
{
    private DeerStateController _stateController = null;
    public void Init(MoveBehavior moveController, DeerStateController stateController)
    {
        _stateController = stateController;
        base.Init(moveController);
    }
    protected override void StateUpdateOnDamage()
    {
        _stateController.CurrentState = DeerState.DAMAGE;
    }
    protected override void StateUpdateOnDeath()
    {
        _stateController.CurrentState = DeerState.DEATH;
    }
}
