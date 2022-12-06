using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnionMainLifeController : MainLifeController
{
    private UnionStateController _stateController = null;
    public void Init(MoveBehavior moveController, UnionStateController stateController)
    {
        _stateController = stateController;
        base.Init(moveController);
    }
    protected override void StateUpdateOnDamage()
    {
        _stateController.CurrentState = UnionState.DAMAGE;
    }
    protected override void StateUpdateOnDeath()
    {
        _stateController.CurrentState = UnionState.DEATH;
    }
}
